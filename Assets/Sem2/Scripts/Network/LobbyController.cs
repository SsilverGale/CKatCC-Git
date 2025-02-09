using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;

public class LobbyController : MonoBehaviour
{
    public static LobbyController Instance;

    //UI Elements
    public Text LobbyNameText;

    //Plyaer Data
    [SerializeField] public GameObject PlayerListViewContent;
    [SerializeField] public GameObject PlayerListItemPrefab;
    [SerializeField] public GameObject LocalPlayerObject;

    //Other Data
    public ulong CurrentLobbyID;
    public bool PlayerItemCreated = false;
    private List<PlayerListItem> PlayerListItems = new List<PlayerListItem>();
    public PlayerObjectController LocalPlayerController;

    //Ready
    public Button StartGameButton;
    public Text ReadyButtonText;


    //Manager
    private CustomNetworkManager manager;

    private CustomNetworkManager Manager{
        get{
            if (manager!= null){
                return manager;
            }
            return manager = CustomNetworkManager.singleton as CustomNetworkManager;
        }
    }

    private void Awake()
    {
        if (Instance == null) {
        Instance = this;
        }
    }
    
    public void ReadyPlayer()
    {
        LocalPlayerController.ChangeReady();
    }
    public void UpdateButton()
    {
        if(LocalPlayerController.Ready)
        {
            ReadyButtonText.text = "Not Ready";
        } else {
            ReadyButtonText.text = "Ready";
        }
    }
    public void CheckIfAllReady()
    {
        bool allReady = false;

        foreach(PlayerObjectController player in Manager.GamePlayers)
        {
            if (player.Ready)
            {
                allReady = true;
            } else {
                allReady = false;
                break;
            }
        }

        if(allReady)
        {
            if(LocalPlayerController.PlayerIDNumber == 1) //Checks if the player is the host
            {
                StartGameButton.interactable = true;
            } else {
                StartGameButton.interactable = false;
            }
        } else {
            StartGameButton.interactable = false;
        }
    }


    public void UpdateLobbyName()
    {
        CurrentLobbyID = Manager.GetComponent<SteamLobby>().CurrentLobbyID;
        LobbyNameText.text = SteamMatchmaking.GetLobbyData( new CSteamID(CurrentLobbyID), "name");
    }
    public void UpdatePlayerList()
    {
        if(!PlayerItemCreated) { CreateHostPlayerItem();} //Host
        if (PlayerListItems.Count < Manager.GamePlayers.Count) { CreateClientPLayerItem();}
        if (PlayerListItems.Count > Manager.GamePlayers.Count) {RemovePLayerItem();}
        if (PlayerListItems.Count == Manager.GamePlayers.Count) {UpdatePlayerItem();}
    }
    public void FindLocalPlayer()
    {
        //LocalPlayerObject = GameObject.Find("LocalGamePlayer"); 
        LocalPlayerController = LocalPlayerObject.GetComponent<PlayerObjectController>();
    }

    public void CreateHostPlayerItem()
    {
        foreach(PlayerObjectController player in Manager.GamePlayers)
        {
            GameObject NewPlayerItem = Instantiate(PlayerListItemPrefab) as GameObject;
            PlayerListItem NewPlayerItemScript = NewPlayerItem.GetComponent<PlayerListItem>();

            NewPlayerItemScript.PlayerName = player.PlayerName;
            NewPlayerItemScript.ConnectionID = player.ConnectionID;
            NewPlayerItemScript.PlayerSteamID = player.PlayerSteamID;
            NewPlayerItemScript.SetPlayerValues();

            NewPlayerItem.transform.SetParent(PlayerListViewContent.transform);
            NewPlayerItem.transform.localScale = Vector3.one;

            PlayerListItems.Add(NewPlayerItemScript);
        }
        PlayerItemCreated = true;
    }
    public void CreateClientPLayerItem()
    {
        foreach(PlayerObjectController player in Manager.GamePlayers)
        {
            if(!PlayerListItems.Any(b => b.ConnectionID == player.ConnectionID)) //Checks if we are alread on list
            {
            GameObject NewPlayerItem = Instantiate(PlayerListItemPrefab) as GameObject;
            PlayerListItem NewPlayerItemScript = NewPlayerItem.GetComponent<PlayerListItem>();

            NewPlayerItemScript.PlayerName = player.PlayerName;
            NewPlayerItemScript.ConnectionID = player.ConnectionID;
            NewPlayerItemScript.PlayerSteamID = player.PlayerSteamID;
            NewPlayerItemScript.Ready = player.Ready;
            NewPlayerItemScript.SetPlayerValues();

            NewPlayerItem.transform.SetParent(PlayerListViewContent.transform);
            NewPlayerItem.transform.localScale = Vector3.one;

            PlayerListItems.Add(NewPlayerItemScript);  
            }
        }
    }
    public void UpdatePlayerItem()
    {
        foreach(PlayerObjectController player in Manager.GamePlayers)
        {
            foreach(PlayerListItem PlayerListItemScript in PlayerListItems)
            {
                if(PlayerListItemScript.ConnectionID == player.ConnectionID)
                {
                    PlayerListItemScript.PlayerName = player.PlayerName;
                    PlayerListItemScript.Ready = player.Ready;
                    PlayerListItemScript.SetPlayerValues();
                    if(player == LocalPlayerController) //Updates each button individually
                    {
                        UpdateButton();
                    }
                }
            }
        }
        CheckIfAllReady();
    }

    public void RemovePLayerItem()
    {
        //Makes a list of poeple to disconnect
        List<PlayerListItem> playerListItemToRemove = new List<PlayerListItem>();

        foreach (PlayerListItem playerlistItem in PlayerListItems)
        {
            if(!Manager.GamePlayers.Any(b=>b.ConnectionID == playerlistItem.ConnectionID))
            {
                playerListItemToRemove.Add(playerlistItem);
            }
        }
        //Removes player for all connected clients
        if(playerListItemToRemove.Count > 0)
            {
            foreach(PlayerListItem playerlistItemToRemove in playerListItemToRemove)
                {
                    GameObject ObjectToRemove = playerlistItemToRemove.gameObject;
                    PlayerListItems.Remove(playerlistItemToRemove);
                    Destroy(ObjectToRemove);
                    ObjectToRemove = null;
                }
            }
        
    }
}

