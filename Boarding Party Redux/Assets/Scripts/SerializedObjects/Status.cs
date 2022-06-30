using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusType
{
    damage,
    speed,
    stun,
    knockback,
    damageOverTime,
    other
}


[System.Serializable]
public class Status : ScriptableObject
{
    public string statusName = "New Status";
    public StatusType statusType;
    public int statusStrength;
    public float statusDuration;
    public float statusRemaining;

    public virtual void ApplyStatusEffectToPlayer(PlayerController player = null)
    {

    }

    public virtual void TickEffect(float i = 0.0f)
    {

    }

    public virtual void ApplyStatusEffectToEnemy(EnemyController enemy = null)
    {

    }
}