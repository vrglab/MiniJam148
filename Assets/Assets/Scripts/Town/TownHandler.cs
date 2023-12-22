using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownHandler : Instancable<TownHandler>
{
    public Dictionary<string, TownBuilding> RegisteredTownBuildings { get; private set; } = new Dictionary<string, TownBuilding>();


    // Start is called before the first frame update
    void Start()
    {
        try
        {
            Settings.Instance.GetSetting("town_status");
        }
        catch (Exception e)
        {
            Settings.Instance.SetSetting("town_status", TownStatus.Alive);
        }
    }
}

public enum TownStatus
{
    Alive,
    UnderAttack,
    Destroyed
}
