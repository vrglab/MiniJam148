using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownStorage : TownBuilding
{
    public override string GetID()
    {
        return "vrglab:/storage";
    }

    public override void OnBuild()
    {
        try
        {
          int current_max_res_storage =  (int)Settings.Instance.GetSetting("town_max_res_storage_amnt");
          Settings.Instance.SetSetting("town_max_res_storage_amnt", current_max_res_storage + 10);
        }
        catch (Exception e)
        {
            Settings.Instance.SetSetting("town_max_res_storage_amnt", 10);
        }
    }

    public override void OnDestroyed()
    {
        int current_max_res_storage = (int)Settings.Instance.GetSetting("town_max_res_storage_amnt");

        if ((current_max_res_storage - 10) > 0)
        {
            Settings.Instance.SetSetting("town_max_res_storage_amnt", current_max_res_storage - 10);
        }
        else
        {
            Settings.Instance.SetSetting("town_max_res_storage_amnt", 0);
        }

        
    }

    public override void TownTick(Dictionary<string, TownBuilding> town)
    {

    }
}
