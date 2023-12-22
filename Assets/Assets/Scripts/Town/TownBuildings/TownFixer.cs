using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownFixer : TownBuilding
{

    [Range(0f, 100f)]
    public float FixBuildingInterval = 60f;

    private float time;

    public void Awake()
    {
        ID = "vrglab:/fixer";
        time = FixBuildingInterval;
    }

    public override void TownTick(Dictionary<string, TownBuilding> town)
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            foreach (var townHall in town.GetTownBuilding("vrglab:/hall"))
            {
                if (townHall.CurrentHealth < townHall.MaxHealth)
                {
                    townHall.HealDamage(1);
                    time = FixBuildingInterval;
                    return;
                }
            }
        }
    }
}
