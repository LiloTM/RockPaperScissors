using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    int i;
    public void GoToNextPhase()
    {
        i++;
        Debug.Log(i);
    }
}
