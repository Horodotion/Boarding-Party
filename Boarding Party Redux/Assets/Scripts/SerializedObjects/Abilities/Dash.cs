using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "abilities/Dash")]
public class Dash : Ability
{
    public Vector3 movementVector;
    public Vector2 storedPlayerMoveAxis;

    public override void UseAbility()
    {
        if (player.moveAxis == Vector2.zero)
        {
            movementVector = new Vector3(storedPlayerMoveAxis.x, 0, storedPlayerMoveAxis.y).normalized;
        }
        else
        {
            movementVector = new Vector3(player.moveAxis.x, 0, player.moveAxis.y).normalized;
        }


        movementVector *= player.playerStats.stat[StatType.speed] * 2;

        // float forwardDirection = Vector3.Dot(movementVector.normalized, player.transform.forward);
        // float sideDirection = Vector3.Dot(movementVector.normalized, player.transform.right);
        Vector3 animatorDirection = player.GetDirectionForAnimator(movementVector);

        player.animator.SetFloat(player.animUpDown, animatorDirection.z);
        player.animator.SetFloat(player.animLeftRight, animatorDirection.x);
    }

    public override void ReduceCooldown(float i)
    {
        if (player.moveAxis != Vector2.zero)
        {
            storedPlayerMoveAxis = player.moveAxis;
        }


        switch (abilityState)
        {
            case (AbilityState.cooldown):
                base.ReduceCooldown(i);
                break;
            
            case (AbilityState.casting):
                base.ReduceCooldown(i);
                Casting();
                break;
        }
    }

    public override void Casting()
    {
        castintTime = Mathf.Clamp(castintTime - Time.deltaTime, 0, Mathf.Infinity);

        player.ourPlayerController.Move(movementVector * Time.deltaTime);

        if (castintTime == 0)
        {
            Deactivate();
        }
    }

    public override void Deactivate()
    {
        player.playerState = PlayerState.inGame;
        abilityState = AbilityState.cooldown;
        player.moveAxis = Vector2.zero;
    }
}
