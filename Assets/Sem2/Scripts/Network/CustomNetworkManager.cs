using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using Steamworks;

public class CustomNetworkManager : NetworkManager
{
    [SerializeField] private PlayerObjectController GamePlayerPrefab;
    public List<PlayerObjectController> GamePlayers {get;} = new List<PlayerObjectController>();
    
    //Function will get called every time a player is added to the server
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        PlayerObjectController GamePlayerInstance = Instantiate(GamePlayerPrefab);
        GamePlayerInstance.ConnectionID = conn.connectionId;
        GamePlayerInstance.PlayerIDNumber = GamePlayers.Count + 1;
        GamePlayerInstance.PlayerSteamID = (ulong)SteamMatchmaking.GetLobbyMemberByIndex((CSteamID)SteamLobby.Instance.CurrentLobbyID, GamePlayers.Count);

        NetworkServer.AddPlayerForConnection(conn, GamePlayerInstance.gameObject);
    }
}
