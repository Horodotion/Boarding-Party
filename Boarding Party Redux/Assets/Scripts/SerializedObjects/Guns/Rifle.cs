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
        
        Renderer[] mats = bullet.GetComponentsInChildren<Renderer>(true);
        if (mats.Length != 0)
        {
            mats[0].material = bullet_m;
            mats[0].gameObject.SetActive(true);
        }

        AddStats(player, bulletScript);

        bulletScript.Launch();
    }
}
