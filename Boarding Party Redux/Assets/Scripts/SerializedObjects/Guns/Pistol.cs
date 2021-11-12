using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Guns/Pistol")]
public class Pistol : Gun
{
    public Gradient gradient;

    public override void Fire(PlayerController player)
    {
        nextTimeToFire = 1 / rateOfFire;

        GameObject bullet = Instantiate(gunProjectilePrefab, player.firePosition.transform.position, player.gameObject.transform.rotation);
        ProjectileScript bulletScript = bullet.GetComponent<ProjectileScript>();
        
        TrailRenderer[] trails = bullet.GetComponentsInChildren<TrailRenderer>(true);
        if (trails.Length != 0)
        {
            trails[0].colorGradient = gradient;
            trails[0].gameObject.SetActive(true);
        }

        AddStats(player, bulletScript);

        bulletScript.Launch();
    }
}
