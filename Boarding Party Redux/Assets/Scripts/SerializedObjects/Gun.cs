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
    public int rateOfFire;
    public Faction hostileFaction;

    public virtual void Fire(PlayerController player = null)
    {
        Debug.Log("This shouldn't happen - Base Gun Function triggered");
    }
}
