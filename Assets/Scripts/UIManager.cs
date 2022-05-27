using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
public enum Hand
{
    Empty,
    Rock,
    Paper,
    Scissors
}

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Button RockButton;
    [SerializeField]
    private Button PaperButton;
    [SerializeField]
    private Button ScissorsButton;
    [SerializeField]
    private Button Play;

    [SerializeField]
    private Hand playHand = Hand.Empty;
    
    private void Start()
    {
        RockButton.onClick.AddListener(() => playHand = Hand.Rock);
        PaperButton.onClick.AddListener(() => playHand = Hand.Paper);
        ScissorsButton.onClick.AddListener(() => playHand = Hand.Scissors);
        
        Play.onClick.AddListener(() => {
            if (playHand != Hand.Empty) 
            {
                //TODO: Maybe put this in an extra class
                if (NetworkManager.Singleton.IsServer)
                    Solver.Instance.getHand(playHand, NetworkManager.Singleton.LocalClientId);
                else Solver.Instance.getHandServerRpc(playHand, NetworkManager.Singleton.LocalClientId);
                playHand = Hand.Empty;
            }
        });
    }
}
