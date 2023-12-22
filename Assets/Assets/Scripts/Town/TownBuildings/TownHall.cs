using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TownHall : TownBuilding
{
    public void Awake()
    {
        ID = "vrglab:/hall";
    }

    public override void TownTick(Dictionary<string, TownBuilding> town)
    {
       
    }
}
