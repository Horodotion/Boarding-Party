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

    public virtual void Fire(PlayerController player = null)
    {
        Debug.Log("This shouldn't happen - Base Gun Function triggered");
    }

    public virtual void AddStats(PlayerController player = null, ProjectileScript bulletScript = null)
    {
        bulletScript.projectileSpeed = gunProjectileSpeed;
        bulletScript.damage += player.damage[0] + gunDamage;
        bulletScript.hostileFaction = hostileFaction;
        bulletScript.lifeSpan = projectileLifeSpan;
    }
}
