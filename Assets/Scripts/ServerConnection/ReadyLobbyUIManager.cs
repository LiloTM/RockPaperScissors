using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using System;
using UnityEngine.SceneManagement;

public class ReadyLobbyUIManager : NetworkBehaviour
{
    [SerializeField]
    private Button readyButton;

    private bool isReady = false;

    void Start()
    {
        readyButton?.onClick.AddListener(()=> 
        {
            isReady = true;
            Debug.Log("Button pressed " + isReady);
            checkIfEveryoneReadyServerRpc();
        });
    }

    [ServerRpc (RequireOwnership = false)]
    public void checkIfEveryoneReadyServerRpc()
    {
        Debug.Log("Reached ServerRpc");
        foreach (ulong uid in NetworkManager.Singleton.ConnectedClientsIds)
        {
            Debug.Log("Inside the for loop");
            if (!NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(uid).GetComponent<ReadyLobbyUIManager>().checkIfReady())
            {
                Debug.Log("in the if in for loop");
                return;
            }

                
        }
        Debug.Log("Outside");
        InitiateGameStartClientRpc();
    }

    [ClientRpc]
    public void InitiateGameStartClientRpc()
    {
        Debug.Log("Game starts...");
        SceneManager.LoadScene("LizardSpock");
    }

    public bool checkIfReady()
    {
        if (isReady) return true;
        else return false;
    }
}
