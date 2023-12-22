using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Tasks.Actions;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AttackTownBuilding : ActionBase
{

    private AIPath AIpathFinder;
    private TownBuilding target;

    protected override void OnInit()
    {
        base.OnInit();
        AIpathFinder = Owner.GetComponent<AIPath>();

        target = TownHandler.Instance.RegisteredTownBuildings.Values.ToArray()[Random.Range(0, TownHandler.Instance.RegisteredTownBuildings.Count+1)];
    }

    protected override TaskStatus OnUpdate()
    {
        AIpathFinder.destination = target.transform.position;
        return TaskStatus.Success;
    }
}
