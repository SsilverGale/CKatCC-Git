using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Unity.Services.Authentication;
using Unity.Services.Core; //Used for unity lobby package
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.VisualScripting;
using UnityEngine;

public class TestLobby : MonoBehaviour
{

    private Lobby hostLobby;
    //This float is for a timer that makes sure lobbise stay visible
    private float heartbeatTimer;

    // Start is called before the first frame update
    private async void Start() {
        //Makes the program pause until getting a server response
        await UnityServices.InitializeAsync();

        //Outputs a log when player signs in
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("signed in" + AuthenticationService.Instance.PlayerId);
        };
        
        //Creates a new account for user and signs in anonymously
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    private void Update() {
        HandleLobbyHeartBeat();
    }
    //Following scrip keeps lobby active
    private void HandleLobbyHeartBeat()
    {
        if (hostLobby != null) //if there is a lobby
        {
            heartbeatTimer -= Time.deltaTime; //Reduce timer
            //When timer hit zero reset time and send a heartbeat ping
            if (heartbeatTimer < 0f) 
            {
                float heartbeatTimerMax = 15;
                heartbeatTimer = heartbeatTimerMax;

                LobbyService.Instance.SendHeartbeatPingAsync(hostLobby.Id);
            }
        }
        
    }

    private async void CreateLobby() {
        //Attempt to create a lobby
        try
        {

            //Set Variables for lobby creation
            string lobbyName = "MyLobby";
            int maxPlayers = 4;

            //Allow you to set up lobby options
            CreateLobbyOptions createLobbyOptions = new CreateLobbyOptions{
                IsLocked = false, //One of the lobby options is making the lobby private
            };


            //Intaniates new lobby
            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers,createLobbyOptions);
            hostLobby = lobby;

            Debug.Log("Created Lobby: " + lobby.Name + " size: " + lobby.MaxPlayers);
        //If there is an error making the lobby, log the error
        } catch (LobbyServiceException e){ 
            Debug.Log(e);
        }
    }
   
    private async void ListLobbies() {
        try
        {

            QueryLobbiesOptions queryLobbiesOptions = new QueryLobbiesOptions
            {
                Count = 25, //Lists 25 results

                //Filters lobbies for ones with open slots
                Filters = new List<QueryFilter> {
                    new QueryFilter(QueryFilter.FieldOptions.AvailableSlots, "0", QueryFilter.OpOptions.GT)
                },
                //Orders lobbies from oldest to newest
                Order = new List<QueryOrder> {
                    new QueryOrder(false, QueryOrder.FieldOptions.Created)
                }
            };

            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync();

            Debug.Log("Lobbies found: " + queryResponse.Results.Count);
            foreach (Lobby lobby in queryResponse.Results)
            {
                Debug.Log(lobby.Name + " " + lobby.MaxPlayers);
            }
        } catch (LobbyServiceException e){
            Debug.Log(e);
            }

    }

 
    private async void JoinLobby() {
        try
        {
            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync();

            await Lobbies.Instance.JoinLobbyByIdAsync(queryResponse.Results[0].Id);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    private async void JoinLobbyByCode(string lobbyCode)
    {
        try
        {
            await Lobbies.Instance.JoinLobbyByCodeAsync(lobbyCode);

            Debug.Log("Joined Lobby with code" + lobbyCode);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    //Quickly joins random lobby
    private async void OnApplicationQuit()
    {
        try {
            await LobbyService.Instance.QuickJoinLobbyAsync();
        } catch (LobbyServiceException e) {
            Debug.Log(e);
        }
    }

    private void PrintPLayers(Lobby lobby) {
        /*
        Debug.Log("Players in Lobby" + lobby.Name);
        foreach (Player player in lobby.Players) {
            Debug.Log(player.Id);
        }
        */
    }
}
