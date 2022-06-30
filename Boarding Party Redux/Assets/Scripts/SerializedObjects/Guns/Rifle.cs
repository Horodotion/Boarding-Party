using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Guns/Rifle")]
public class Rifle : Gun
{    
    public Material bullet_m;

    public override void Fire(PlayerController player)
    {
        nextTimeToFire = 1 / rateOfFire;

        GameObject bullet = Instantiate(gunProjectilePrefab, player.firePosition.transform.position, player.gameObject.transform.rotation);
        ProjectileScript bulletScript = bullet.GetComponent<ProjectileScript>();
        bullet.GetComponentInChildren<Renderer>().material = bullet_m;

        AddStats(player, bulletScript);
        bulletScript.Launch();
    }
}
