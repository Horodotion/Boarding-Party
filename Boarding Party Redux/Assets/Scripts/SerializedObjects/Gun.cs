using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Gun : ScriptableObject
{
    public string gunName = "New Gun";
    public GameObject gunProjectilePrefab;
    public int gunDamage;
    public int gunProjectileSpeed;
    public int projectileLifeSpan;
    public float rateOfFire;
    public float nextTimeToFire;
    public Faction hostileFaction;
    public List<Status> statusEffects;

    public virtual void Fire(PlayerController player = null)
    {

    }

    public virtual void AddStats(PlayerController player = null, ProjectileScript bulletScript = null)
    {
        bulletScript.projectileSpeed = gunProjectileSpeed;
        bulletScript.hostileFaction = hostileFaction;
        bulletScript.lifeSpan = projectileLifeSpan;

        if (statusEffects.Count != 0)
        {
            for (int i = 0; i < statusEffects.Count; i++)
            {
                Status newStatus = Instantiate(statusEffects[i]);
                bulletScript.ourStatusEffects.Add(newStatus);
                if (newStatus.statusType == StatusType.damage || newStatus.statusType == StatusType.damageOverTime)
                {
                    newStatus.statusStrength += gunDamage; // + (int)player.playerStats.stat[StatType.damage];
                }
            }
        }
    }

    public virtual void AddStatsFromEnemy(EnemyController enemy = null, ProjectileScript bulletScript = null)
    {
        bulletScript.projectileSpeed = gunProjectileSpeed;
        bulletScript.hostileFaction = hostileFaction;
        bulletScript.lifeSpan = projectileLifeSpan;

        if (statusEffects.Count != 0)
        {
            for (int i = 0; i < statusEffects.Count; i++)
            {
                Status newStatus = Instantiate(statusEffects[i]);
                bulletScript.ourStatusEffects.Add(newStatus);
                if (newStatus.statusType == StatusType.damage || newStatus.statusType == StatusType.damageOverTime)
                {
                    newStatus.statusStrength += gunDamage;
                }
            }
        }
    }
}