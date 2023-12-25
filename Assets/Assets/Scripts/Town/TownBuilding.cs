using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TownBuilding : Creature
{

    public abstract string GetID();
    public abstract void TownTick(Dictionary<string, TownBuilding> town);
    public abstract void OnBuild();
    public abstract void OnDestroyed();

    public void Start()
    {
        string id = $"{GetID()}:{TownHandler.Instance.RegisteredTownBuildings.Count}";
        TownHandler.Instance.RegisteredTownBuildings.Add(id, this);
        CurrentHealth = MaxHealth;
        OnDeath.AddListener(OnDestroyed);
        OnBuild();
    }

    public void Update()
    {
        base.Update();
        if(!IsDead)
        {
            TownTick(TownHandler.Instance.RegisteredTownBuildings);
        }
    }

    protected override void Movement()
    {
        
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
