using System.Collections;
using System.Collections.Generic;
using Unity.Netcode; //Used for network manager reference
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button serverBtn;
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button clientBtn;

    private void Awake()
    {
        //Lets someone create a deditcated server using the server button
        //we probably won't actuallt be using this
        serverBtn.onClick.AddListener(() => {
            NetworkManager.Singleton.StartServer();
        });
        //Lets a player begin hosting a multiplayer game with host button
        hostBtn.onClick.AddListener(() => {
            NetworkManager.Singleton.StartHost();
        });
        //Lets a player join a host as a client using the Client button
        clientBtn.onClick.AddListener(() => {
            NetworkManager.Singleton.StartClient();
        });
    }
}
