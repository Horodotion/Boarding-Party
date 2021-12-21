using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Guns/Bolt")]
public class Bolt : Gun
{
    public Gradient gradient;

    public override void Fire(PlayerController player)
    {
        nextTimeToFire = 1 / rateOfFire;

        GameObject bullet = Instantiate(gunProjectilePrefab, player.firePosition.transform.position, player.gameObject.transform.rotation);
        ProjectileScript bulletScript = bullet.GetComponent<ProjectileScript>();
        bullet.GetComponentInChildren<TrailRenderer>().colorGradient = gradient;

        AddStats(player, bulletScript);

        bulletScript.Launch();
    }
}
