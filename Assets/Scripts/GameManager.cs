using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GameManager : NetworkBehaviour
{
    [SerializeField]
    private int winCount = 0;

    [ClientRpc]
    public void increaseWinCountClientRpc()
    {
        Debug.Log("Win++");
        winCount++;
        if (winCount >= 3) ClientWinServerRpc();
    }

    //TODO: DAS FUNKTIONIERT NOCH NICHT
    [ServerRpc(RequireOwnership = false)]
    private void ClientWinServerRpc()
    {
        if(IsOwner)
            Debug.Log("You won");
        else
            Debug.Log("You lost");
        //TODO: Win and Lose of Players
    }
}
