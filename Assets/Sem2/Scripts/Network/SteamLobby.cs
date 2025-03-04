using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;
using UnityEngine.UI;
using System.Xml.Serialization;

public class SteamLobby : MonoBehaviour
{
    public static SteamLobby Instance;
    
    //Callbacks
    //Callbacks are functions called whenever something happens with steam
    protected Callback<LobbyCreated_t> LobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> JoinRequest;
    protected Callback<LobbyEnter_t> LobbyEntered;

    //Variables
    public ulong CurrentLobbyID;
    private const string HostAddressKey = "HostAddress";
    private CustomNetworkManager manager;

    //Game Object
    public GameObject HostButton;
    public Text LobbyNameText;

    private void Start()
    {
        //Checks if steam is open and only runs code if it is
        if(!SteamManager.Initialized) { return;}
        if (Instance == null) {Instance = this;}
        //initialize functions
        manager = GetComponent<CustomNetworkManager>();
        LobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        JoinRequest = Callback<GameLobbyJoinRequested_t>.Create(OnJoinRequest);
        LobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
    }
    public void HostLobby(){
        //Creates a friends only lobby with a variable amount of max connections
        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly,manager.maxConnections);
    }

    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        //Checks for and error and cancels if there is one
        if(callback.m_eResult != EResult.k_EResultOK) {return;}
        
        Debug.Log("LobbyCreatedSuccessfully");
        //Start Hosting Game
        manager.StartHost();

        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey, SteamUser.GetSteamID().ToString());
        //Set lobby name
        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "name", SteamFriends.GetPersonaName().ToString() + "'s lobby");
    }

    private void OnJoinRequest (GameLobbyJoinRequested_t callback){
        Debug.Log("Request To Join Lobby");
        SteamMatchmaking.JoinLobby(callback.m_steamIDFriend);
    }

    private void OnLobbyEntered(LobbyEnter_t callback){
        //Everyone
        CurrentLobbyID = callback.m_ulSteamIDLobby;
        LobbyNameText.gameObject.SetActive(true);
        LobbyNameText.text = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "name");
        
        //Clients
        if(NetworkServer.active) {return;}

        manager.networkAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey);

        manager.StartClient();
    }
}
