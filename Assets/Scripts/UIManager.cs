using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private Hand playHand = Hand.Empty;

    private void Start()
    {
        RockButton.onClick.AddListener(() => playHand = Hand.Rock);
        PaperButton.onClick.AddListener(() => playHand = Hand.Paper);
        ScissorsButton.onClick.AddListener(() => playHand = Hand.Scissors);
        
        Play.onClick.AddListener(() => {
            if (playHand != Hand.Empty) 
            { 
                // TODO: here the hand needs to be given to the Server-Solver. RESEARCH!
                Debug.Log(playHand);
                playHand = Hand.Empty;
            }
        });
    }
}
