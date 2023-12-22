using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TownBuilding : Creature
{
    public string ID { get; protected set; } = "vrglab:/Base";
    public abstract void TownTick(Dictionary<string, TownBuilding> town);

    public void Start()
    {
        string id = $"{ID}:{TownHandler.Instance.RegisteredTownBuildings.Count}";
        TownHandler.Instance.RegisteredTownBuildings.Add(id, this);
        CurrentHealth = MaxHealth;
    }

    public void Update()
    {
        base.Update();
        TownTick(TownHandler.Instance.RegisteredTownBuildings);
    }

    protected override void Movement()
    {
        
    }
}
