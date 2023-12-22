using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TownHall : TownBuilding
{

    public override string GetID()
    {
        return "vrglab:/hall";
    }

    public override void OnBuild()
    {

    }

    public override void OnDestroyed()
    {

    }

    public override void TownTick(Dictionary<string, TownBuilding> town)
    {
       
    }
}
