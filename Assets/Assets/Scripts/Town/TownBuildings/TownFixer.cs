using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownFixer : TownBuilding
{

    [Range(0f, 100f)]
    public float FixBuildingInterval = 60f;

    private float time;

    public override string GetID()
    {
        return "vrglab:/fixer";
    }

    public override void OnBuild()
    {
        time = FixBuildingInterval;
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
