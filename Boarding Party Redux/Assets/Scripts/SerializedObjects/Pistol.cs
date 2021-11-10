using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Guns/Pistol")]
public class Pistol : Gun
{
    public override void Fire(PlayerController player)
    {
        GameObject bullet = Instantiate(gunProjectilePrefab, player.firePosition.transform.position, player.gameObject.transform.rotation);
        ProjectileScript ourProjectileScript = bullet.GetComponent<ProjectileScript>();

        ourProjectileScript.projectileSpeed = gunProjectileSpeed;
        ourProjectileScript.damage += player.damage[0] + gunDamage;
        ourProjectileScript.hostileFaction = hostileFaction;
    }
}
