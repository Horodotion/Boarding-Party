using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public float cooldown;
    public float ready;
    public float castingTime;
    public int stacks;
    public PlayerClass classSpecific = PlayerClass.basic;
    
    public virtual void ActivateAbility(PlayerController player = null)
    {

    }

    public virtual void ReduceCooldown()
    {

    }

    public virtual void Fire(PlayerController player = null)
    {

    }

    public virtual void AddStats(PlayerController player = null, ProjectileScript bulletScript = null)
    {

    }
}
