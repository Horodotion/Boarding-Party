using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "abilities/Grenade")]
public class ExplosiveAbility : Ability
{
    public GameObject explosive;
    public float damage, explosiveLifetime, explosiveProjectileSpeed;
    public int explosiveDamage;

    public override void Activate(float i = 0)
    {
        if (stacks > 0)
        {
            Fire(player);
            base.Activate(i);
        }
    }

    public override void Fire(PlayerController player)
    {

        GameObject bullet = Instantiate(explosive, player.firePosition.transform.position, player.gameObject.transform.rotation);
        ProjectileScript bulletScript = bullet.GetComponent<ProjectileScript>();

        bulletScript.projectileSpeed = explosiveProjectileSpeed;
        bulletScript.hostileFaction = Faction.Enemy;
        bulletScript.lifeSpan = explosiveLifetime;
        bulletScript.damage = explosiveDamage;
        
        if (player != null)
        {
            bulletScript.ourPlayer = player;
        }

        bulletScript.Launch();
    }
}
