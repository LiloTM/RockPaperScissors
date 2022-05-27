using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Solver : NetworkBehaviour
{
    [SerializeField]
    private NetworkVariable<Hand> FirstPlayer = new NetworkVariable<Hand>();
    [SerializeField]
    private NetworkVariable<Hand> SecondPlayer = new NetworkVariable<Hand>();

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

    private void Update()
    {
        if (FirstPlayer.Value != Hand.Empty && SecondPlayer.Value != Hand.Empty)
        {
            Debug.Log("Test");
            ExecuteSolver();
            FirstPlayer.Value = Hand.Empty;
            SecondPlayer.Value = Hand.Empty;
        }
    }

    public void ExecuteSolver()
    {
        if (IsServer)
        {
            CompareHands();

        }
    }

    private void CompareHands()
    {
        if (FirstPlayer.Value == SecondPlayer.Value) Debug.Log("Gleichstand");
        else if(FirstPlayer.Value == Hand.Rock)
        {
            if (SecondPlayer.Value == Hand.Paper) 
                Debug.Log("SecondPlayer " + SecondPlayer.Value + " wins against " + FirstPlayer.Value);
            else Debug.Log("FirstPlayer " + FirstPlayer.Value + " wins against " + SecondPlayer.Value);
        }
        else if (FirstPlayer.Value == Hand.Paper)
        {
            if (SecondPlayer.Value == Hand.Scissors) 
                Debug.Log("SecondPlayer " + SecondPlayer.Value + " wins against " + FirstPlayer.Value);
            else Debug.Log("FirstPlayer " + FirstPlayer.Value + " wins against " + SecondPlayer.Value);
        }
        else if (FirstPlayer.Value == Hand.Scissors)
        {
            if (SecondPlayer.Value == Hand.Rock)
                Debug.Log("SecondPlayer " + SecondPlayer.Value + " wins against " + FirstPlayer.Value);
            else Debug.Log("FirstPlayer " + FirstPlayer.Value + " wins against " + SecondPlayer.Value);
        }
    }

    public void getHand(Hand hand)
    {
        FirstPlayer = new NetworkVariable<Hand>(hand);
        Debug.Log("FirstPlayer has been sent to the Solver.");
    }
    
    [ServerRpc(RequireOwnership = false)]
    public void getHandServerRpc(Hand hand)
    {
        SecondPlayer = new NetworkVariable<Hand>(hand);
        Debug.Log("SecondPlayer has been sent to the Solver.");
    }
}
