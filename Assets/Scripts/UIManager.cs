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
                if (NetworkManager.Singleton.IsServer)
                    Solver.Instance.getHand(playHand);
                else Solver.Instance.getHandServerRpc(playHand);

                Debug.Log(playHand );
                playHand = Hand.Empty;
            }
        });
    }
}
