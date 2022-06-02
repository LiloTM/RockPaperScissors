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
    Scissors,
    Lizard,
    Spock
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
    private Button LizardButton;
    [SerializeField]
    private Button SpockButton;

    [SerializeField]
    private Button Play;

    private TMPro.TMP_Text winLose;
    private TMPro.TMP_Text winCountText;
    private IEnumerator coroutine;

    [SerializeField]
    private Hand playHand = Hand.Empty;
    
    private void Start()
    {
        winLose = (TMPro.TMP_Text)GameObject.Find("Win/Lose").GetComponent(typeof(TMPro.TMP_Text));
        winLose.text = "";
        winCountText = (TMPro.TMP_Text)GameObject.Find("WinCountText").GetComponent(typeof(TMPro.TMP_Text));
        winCountText.text = "";

        RockButton.onClick.AddListener(() => playHand = Hand.Rock);
        PaperButton.onClick.AddListener(() => playHand = Hand.Paper);
        ScissorsButton.onClick.AddListener(() => playHand = Hand.Scissors);
        LizardButton.onClick.AddListener(() => playHand = Hand.Lizard);
        SpockButton.onClick.AddListener(() => playHand = Hand.Spock);

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

    public void setRoundStats(string infoText)
    {
        coroutine = WaitAndPrint(3.5f);
        winLose.text = infoText;
        StartCoroutine(coroutine);
    }

    public void setWinCount(int wins)
    {
        winCountText.text = "Wins: " + wins;
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            winLose.text = "";
            StopCoroutine(coroutine);
        }
    }

    public void setWinLose(bool win)
    {
        if (win) winLose.text = "You Win";
        else winLose.text = "You Lose";
    }
}
