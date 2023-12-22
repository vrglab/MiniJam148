using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownFarm : TownBuilding
{
    [Range(0f, 100f)]
    public float IncreaseResInterval = 60f;

    private float time;

    public override string GetID()
    {
        return "vrglab:/farm";
    }

    public override void OnBuild()
    {
        time = IncreaseResInterval;
        try
        {
            Settings.Instance.GetSetting("town_stored_res");
        }
        catch (Exception e)
        {
            Settings.Instance.SetSetting("town_stored_res", 0);
        }
    }

    public override void OnDestroyed()
    {

    }

    public override void TownTick(Dictionary<string, TownBuilding> town)
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            int current_stored_res = (int)Settings.Instance.GetSetting("town_stored_res"), current_max_res_amt = (int)Settings.Instance.GetSetting("town_max_res_storage_amnt");
            if ((current_stored_res + 1) < current_max_res_amt)
            {
                Settings.Instance.SetSetting("town_stored_res", current_stored_res + 1);
            }
            time = IncreaseResInterval;
        }
    }
}
