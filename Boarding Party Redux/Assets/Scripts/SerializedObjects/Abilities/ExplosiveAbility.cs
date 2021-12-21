using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "abilities/Grenade")]
public class ExplosiveAbility : Ability
{
    public Gun explosive;
    public override void Activate(PlayerController player = null, float i = 0)
    {
        if (stacks > 0)
        {
            explosive.Fire();
            base.Activate(player, i);
        }
    }
}
