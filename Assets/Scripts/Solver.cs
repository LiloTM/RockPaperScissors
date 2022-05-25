using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solver : MonoBehaviour
{
    Hand FirstPlayer;
    Hand SecondPlayer;

    public void CompareHands()
    {
        if (FirstPlayer == SecondPlayer) Debug.Log("Gleichstand");
        else if(FirstPlayer == Hand.Rock)
        {
            if (SecondPlayer == Hand.Paper) Debug.Log("SecondPlayer " + SecondPlayer + " wins against " + FirstPlayer);
            else Debug.Log("FirstPlayer " + FirstPlayer + " wins against " + SecondPlayer);
        }
        else if (FirstPlayer == Hand.Paper)
        {
            if (SecondPlayer == Hand.Scissors) Debug.Log("SecondPlayer " + SecondPlayer + " wins against " + FirstPlayer);
            else Debug.Log("FirstPlayer " + FirstPlayer + " wins against " + SecondPlayer);
        }
        else if (FirstPlayer == Hand.Scissors)
        {
            if (SecondPlayer == Hand.Rock) Debug.Log("SecondPlayer " + SecondPlayer + " wins against " + FirstPlayer);
            else Debug.Log("FirstPlayer " + FirstPlayer + " wins against " + SecondPlayer);
        }
    }

    public void getHand1(Hand hand)
    {
        FirstPlayer = hand;
    }
    public void getHand2(Hand hand)
    {
        SecondPlayer = hand;
    }
}
