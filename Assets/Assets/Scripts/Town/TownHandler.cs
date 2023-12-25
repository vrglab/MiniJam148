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

    private void Update()
    {
        TownStatus status = (TownStatus)Settings.Instance.GetSetting("town_status");
        if ( status == TownStatus.UnderAttack)
        {
            int total_dead_buildings = 0;
            foreach (var townBuilding in RegisteredTownBuildings)
            {
                if (townBuilding.Value.IsDead)
                {
                    total_dead_buildings++;
                }
            }

            if (total_dead_buildings >= RegisteredTownBuildings.Count)
            {
                Settings.Instance.SetSetting("town_status", TownStatus.Destroyed);
            }
        }
    }
}

public enum TownStatus
{
    Alive,
    UnderAttack,
    Destroyed
}
