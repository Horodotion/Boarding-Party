using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "abilities/Dash")]
public class Dash : Ability
{
    public Vector3 movementVector;
    public override void Activate(PlayerController player = null, float i = 0)
    {
        if (stacks > 0)
        {
            movementVector = new Vector3(player.moveAxis.x, 0, player.moveAxis.y).normalized * player.playerStats.stat[StatType.speed] * 2;
            base.Activate(player, i);
        }
    }

    public override void ReduceCooldown(float i, PlayerController player = null)
    {
        switch (abilityState)
        {
            case (AbilityState.cooldown):
                base.ReduceCooldown(i, null);
                break;
            
            case (AbilityState.casting):
                base.ReduceCooldown(i, null);
                Casting(player);
                break;
        }

    }

    public override void Casting(PlayerController player = null)
    {
        castintTime = Mathf.Clamp(castintTime - Time.deltaTime, 0, Mathf.Infinity);

        player.ourPlayerController.Move(movementVector * Time.deltaTime);

        if (castintTime == 0)
        {
            Deactivate(player);
        }
    }

    public override void Deactivate(PlayerController player = null)
    {
        player.playerState = PlayerState.inGame;
        abilityState = AbilityState.cooldown;
    }
}
