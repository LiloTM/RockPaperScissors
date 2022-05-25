using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class UIManager : NetworkBehaviour
{
    public void Rock()
    {
        Debug.Log(OwnerClientId + " Rock");
    }
    public void Paper()
    {
        Debug.Log(OwnerClientId + " Paper");
    }
    public void Scissors()
    {
        Debug.Log(OwnerClientId + " Scissors");
    }
}
