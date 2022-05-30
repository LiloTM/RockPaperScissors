using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GameManager : NetworkBehaviour
{
    [SerializeField]
    private int winCount = 0;
    private UIManager uiManager;

    private void Start()
    {
        uiManager = (UIManager)FindObjectOfType(typeof(UIManager));
    }

    [ClientRpc]
    public void setUIClientRpc(string infoText)
    {
        uiManager.setRoundStats(infoText);
    }

    [ClientRpc]
    public void increaseWinCountClientRpc()
    {
        Debug.Log("Win++");
        winCount++;
        if (winCount >= 3) ClientWinServerRpc(NetworkManager.Singleton.LocalClientId);
    }

    [ServerRpc(RequireOwnership = false)]
    private void ClientWinServerRpc(ulong winnerID)
    {
        WinClientRpc(winnerID);
    }

    [ClientRpc]
    private void WinClientRpc(ulong ID)
    {
        if(ID == NetworkManager.Singleton.LocalClientId)
            uiManager.setWinLose(true);
        else uiManager.setWinLose(false);
    }
}

