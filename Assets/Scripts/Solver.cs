using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Solver : NetworkBehaviour
{
    [SerializeField]
    private NetworkVariable<Hand> HostPlayer = new NetworkVariable<Hand>();
    [SerializeField]
    private NetworkVariable<Hand> ClientPlayer = new NetworkVariable<Hand>();

    private ulong HostID;
    private ulong ClientID;

    private string infoText;

    #region Singleton
    private static Solver _instance;
    public static Solver Instance
    {
        get
        {
            if (_instance == null)
            {
                var objs = FindObjectsOfType(typeof(Solver)) as Solver[];
                if (objs.Length > 0)
                    _instance = objs[0];
                if (objs.Length > 1)
                {
                    Debug.LogError("There is more than one " + typeof(Solver).Name + " in the scene.");
                }
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = string.Format("_{0}", typeof(Solver).Name);
                    _instance = obj.AddComponent<Solver>();
                }
            }
            return _instance;
        }
    }
    #endregion 

    private void isPlayed()
    {
        if (HostPlayer.Value != Hand.Empty && ClientPlayer.Value != Hand.Empty)
        {
            if (IsServer)
            {
                CompareHands();
                setUI();
            }
            HostPlayer.Value = Hand.Empty;
            ClientPlayer.Value = Hand.Empty;
        }
    }

    private void CompareHands()
    {
        if (HostPlayer.Value == ClientPlayer.Value) infoText = ("Gleichstand"); 
        else if (HostPlayer.Value == Hand.Rock)
        {
            if (ClientPlayer.Value == Hand.Paper) ClientWins();
            else if (ClientPlayer.Value == Hand.Spock) ClientWins();
            else if (ClientPlayer.Value == Hand.Scissors) HostWins();
            else if (ClientPlayer.Value == Hand.Lizard) HostWins();
        }
        else if (HostPlayer.Value == Hand.Paper)
        {
            if (ClientPlayer.Value == Hand.Scissors) ClientWins();
            else if (ClientPlayer.Value == Hand.Lizard) ClientWins();
            else if (ClientPlayer.Value == Hand.Rock) HostWins();
            else if (ClientPlayer.Value == Hand.Spock) HostWins();
        }
        else if (HostPlayer.Value == Hand.Scissors)
        {
            if (ClientPlayer.Value == Hand.Rock) ClientWins();
            else if (ClientPlayer.Value == Hand.Spock) ClientWins();
            else if (ClientPlayer.Value == Hand.Paper) HostWins();
            else if (ClientPlayer.Value == Hand.Lizard) HostWins();
        }
        else if (HostPlayer.Value == Hand.Lizard)
        {
            if (ClientPlayer.Value == Hand.Rock) ClientWins();
            else if (ClientPlayer.Value == Hand.Scissors) ClientWins();
            else if (ClientPlayer.Value == Hand.Spock) HostWins();
            else if (ClientPlayer.Value == Hand.Paper) HostWins();
        }
        else if (HostPlayer.Value == Hand.Spock)
        {
            if (ClientPlayer.Value == Hand.Lizard) ClientWins();
            else if (ClientPlayer.Value == Hand.Paper) ClientWins();
            else if (ClientPlayer.Value == Hand.Scissors) HostWins();
            else if (ClientPlayer.Value == Hand.Rock) HostWins();
        }
    }

    private void ClientWins()
    {
        NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(ClientID).GetComponent<GameManager>().increaseWinCountClientRpc();
        infoText = "Client " + ClientPlayer.Value + " wins against " + HostPlayer.Value;
    }
    private void HostWins()
    {
        NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(HostID).GetComponent<GameManager>().increaseWinCountClientRpc();
        infoText = "Host " + HostPlayer.Value + " wins against " + ClientPlayer.Value;
    }

    private void setUI()
    {
        NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(HostID).GetComponent<GameManager>().setUIClientRpc(infoText);
        NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(ClientID).GetComponent<GameManager>().setUIClientRpc(infoText);
    }

    public void getHand(Hand hand, ulong ID)
    {
        HostPlayer = new NetworkVariable<Hand>(hand); 
        HostID = ID;
        Debug.Log("Host " + HostID + " has been sent to the Solver. " + HostPlayer.Value);
        isPlayed();
    }
    
    [ServerRpc(RequireOwnership = false)]
    public void getHandServerRpc(Hand hand, ulong ID)
    {
        ClientPlayer = new NetworkVariable<Hand>(hand);
        ClientID = ID;
        Debug.Log("Client " + ClientID + " has been sent to the Solver. " + ClientPlayer.Value);
        isPlayed();
    }
}
