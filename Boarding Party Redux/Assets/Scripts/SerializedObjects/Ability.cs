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
    public PlayerController player;
    public float cooldown;
    public float cooldownDuration;
    public float castingDuration;
    public float castintTime;
    public int stacks;
    public int stacksMax;
    public PlayerClass classSpecific = PlayerClass.basic;
    
    public virtual void Activate(float i = 0)
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
    
    public virtual void Casting()
    {
        castintTime = Mathf.Clamp(castintTime - Time.deltaTime, 0, Mathf.Infinity);
        if (castintTime == 0)
        {
            Deactivate();
        }
    }

    public virtual void Deactivate()
    {

    }

    public virtual void ReduceCooldown(float i)
    {
        if (stacks < stacksMax)
        {  
            if (cooldown > 0)
            {
                cooldown = GeneralManager.ReduceToZeroByTime(cooldown);
            }
            if (cooldown <= 0)
            {
                stacks++;
            }
        }
    }

    public virtual void Fire()
    {

    }

    public virtual void AddStats(ProjectileScript bulletScript = null)
    {

    }
}
