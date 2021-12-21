using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityState
{
    casting,
    cooldown,
    idle
}
public abstract class Ability : ScriptableObject
{
    public AbilityState abilityState = AbilityState.cooldown;
    public float cooldown;
    public float cooldownDuration;
    public float castingDuration;
    public float castintTime;
    public int stacks;
    public int stacksMax;
    public PlayerClass classSpecific = PlayerClass.basic;
    
    public virtual void Activate(PlayerController player = null, float i = 0)
    {
        castintTime = castingDuration;
        player.playerState = PlayerState.idle;
        abilityState = AbilityState.casting;
        
        stacks--;
        if (stacks <= 0)
        {
            stacks = 0;
        }
    }
    
    public virtual void Casting(PlayerController player = null)
    {
        castintTime = Mathf.Clamp(castintTime - Time.deltaTime, 0, Mathf.Infinity);
        if (castintTime == 0)
        {
            Deactivate();
        }
    }

    public virtual void Deactivate(PlayerController player = null)
    {

    }

    public virtual void ReduceCooldown(float i, PlayerController player = null)
    {
        if (stacks < stacksMax)
        {  
            if (cooldown != 0)
            {
                cooldown = MasterManager.ReduceToZero(cooldown, i);
            }
            if (cooldown == 0)
            {
                stacks++;
            }
        }
    }

    public virtual void Fire(PlayerController player = null)
    {

    }

    public virtual void AddStats(PlayerController player = null, ProjectileScript bulletScript = null)
    {

    }
}
