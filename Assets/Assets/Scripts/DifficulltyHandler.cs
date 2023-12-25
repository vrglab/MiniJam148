using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DifficulltyHandler : MonoBehaviour
{
    public List<Difficulty> difficultyList;

    void Update()
    {
        int current_m = (int)Settings.Instance.GetSetting("town_stored_mon");
        foreach (Difficulty difficulty in difficultyList)
        {
            if(!difficulty.Reached)
            {
                if(current_m > difficulty.start_when_money_reached)
                {
                    difficulty.turnOnWhenReached.Invoke();
                    difficulty.reached();
                }
            }
        }
    }

    public void EnterHardMode()
    {
        Settings.Instance.SetSetting("town_hard_mode", true);
    }
}

[Serializable]
public struct Difficulty
{
    public int start_when_money_reached;
    public UnityEvent turnOnWhenReached;

    public void reached()
    {
        Reached = true;
    }

    public bool Reached { get; set; }
}
