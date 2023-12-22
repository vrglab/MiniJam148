using CleverCrow.Fluid.BTs.Trees;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class BehaviourTreeExtensions
{
    public static BehaviorTreeBuilder TurnMovementOff(this BehaviorTreeBuilder builder)
    {
        return builder.AddNode(new TurnMovementOff());
    }

    public static BehaviorTreeBuilder AttackTown(this BehaviorTreeBuilder builder)
    {
        return builder.AddNode(new AttackTownBuilding());
    }
}
