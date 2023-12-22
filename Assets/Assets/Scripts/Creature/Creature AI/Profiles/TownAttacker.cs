using CleverCrow.Fluid.BTs.Trees;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ai Profiles/Town Attacker")]
public class TownAttacker : AiProfile
{
    public override BehaviorTreeBuilder BuildBehaviour(GameObject owner)
    {
        return base.BuildBehaviour(owner).Sequence().AttackTown().End();
    }
}
