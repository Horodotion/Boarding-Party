using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Guns/Grenade")]
public class Grenade : Gun
{
    public override void Fire(PlayerController player)
    {
        nextTimeToFire = 1 / rateOfFire;

        GameObject bullet = Instantiate(gunProjectilePrefab, player.firePosition.transform.position, player.gameObject.transform.rotation);
        ProjectileScript bulletScript = bullet.GetComponent<ProjectileScript>();

        AddStats(player, bulletScript);

        bulletScript.Launch();
    }
}