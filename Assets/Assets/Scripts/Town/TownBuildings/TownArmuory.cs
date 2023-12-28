using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownArmuory : TownBuilding
{

    [Range(0f, 100f)]
    public float IncreaseAmmo = 60f;

    private float time;

    public override string GetID()
    {
        return "vrglab:/armuory";
    }

    public override void OnBuild()
    {
        time = IncreaseAmmo;
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
            foreach (var townFighter in town.GetTownBuilding<TownFighter>("vrglab:/fighter"))
            {
                if (townFighter.weaponManager.gunHandler.GetCurrentAmmo() < townFighter.weaponManager.gunHandler.GetMaxAmmo())
                {
                    townFighter.weaponManager.gunHandler.IncreaseAmmoBy(12);
                    time = IncreaseAmmo;
                    return;
                }
            }
            time = IncreaseAmmo;
        }
        
    }
}
