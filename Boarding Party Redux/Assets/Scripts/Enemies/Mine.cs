using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : EnemyController
{
    [Header("Explosion Variables")]
    public int damage;
    public float explosionRadius;
    public GameObject destroyEffectPrefab;
    public float secondsUntilParticlesAreDestroyed;

    public override void Aggro()
    {
        if (!DetectPlayers() || targettedPlayer == null)
        {
            currentState = EnemyState.searching;
            lastDetectedPlayer = lastDetectedPlayerDuration;
        }
        else if (Vector3.Distance(transform.position, targettedPlayer.transform.position) <= explosionRadius * 0.66)
        {
            CommitDie();
        }
        else
        {
            navAgent.SetDestination(targettedPlayer.transform.position);
        }
    }

    public override void CommitDie(PlayerController playerCreditedForKill = null)
    {
        GivePoints((int)enemyStats.stat[StatType.score], playerCreditedForKill);
    
        dead = true;
        Explode();
        Destroy(gameObject);
        Debug.Log("Dead");
    }

    public void Explode()
    {
        GameObject destructionParticles = Instantiate(destroyEffectPrefab, transform.position, Quaternion.Euler(0, 0, 0));
        Destroy(destructionParticles, secondsUntilParticlesAreDestroyed);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hC in hitColliders)
        {
            // INSERT HERE: Function or however damage is assigned, pass each object returned in hitColliders the damage variable above
            if (hC.gameObject.tag == "Player" && hC.GetComponent<PlayerController>() != null)
            {
                hC.GetComponent<PlayerController>().ChangeHealth(-damage);
            }
            if (hC.gameObject.tag == "Enemy" && hC.GetComponent<EnemyController>() != null && !hC.GetComponent<EnemyController>().dead)
            {
                hC.GetComponent<EnemyController>().ChangeHealth(damage);
            }

            // Optional addition: Explosion force equal to damage, originating from this object's position. Remove if not wanted
            if (hC.GetComponent<Rigidbody>() != null)
            {
                hC.GetComponent<Rigidbody>().AddExplosionForce(damage * 5, transform.position, explosionRadius, 1f);
            }
        }
    }
}
