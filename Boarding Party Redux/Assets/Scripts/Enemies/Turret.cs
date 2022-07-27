using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : EnemyController
{
    [Header("Turret Specific variables")]
    public Transform turretHead;
    public float turretRotationSpeed;
    private string activateAnim = "Activate";

    [Header("Projectile Variables")]
    public GameObject projectilePrefab;
    public Gradient colorGradient;
    [HideInInspector] public List<Transform> firePositions;
    // public float firingSpeed;
    [HideInInspector] public float nextTimeToFire;
    [HideInInspector] public int currentFiringPosition = 0;

    public override void Awake()
    {
        base.Awake();
        
        foreach (SphereCollider sphere in GetComponentsInChildren<SphereCollider>())
        {
            firePositions.Add(sphere.gameObject.transform);
        }
    }

    public override void Idle()
    {
        base.Idle();

        if (nextTimeToFire > 0)
        {
            nextTimeToFire = GeneralManager.ReduceToZeroByTime(nextTimeToFire);
        }
        if (enemyAnim != null)
        {
            
        }
    }

    public override void Searching()
    {
        if (DetectPlayers())
        {
            currentState = EnemyState.aggro;
        }

        if (nextTimeToFire > 0)
        {
            nextTimeToFire = GeneralManager.ReduceToZeroByTime(nextTimeToFire);
        }
    }

    public override void Aggro()
    {
        if (!DetectPlayers() || targettedPlayer == null)
        {
            currentState = EnemyState.searching;
            lastDetectedPlayer = lastDetectedPlayerDuration;
        }
        else if (nextTimeToFire <= 0)
        {
            Shoot();
        }
        else
        {
            nextTimeToFire = GeneralManager.ReduceToZeroByTime(nextTimeToFire);
            
            Vector3 relativePlayerPosition = new Vector3(targettedPlayer.transform.position.x, turretHead.position.y, targettedPlayer.transform.position.z);
            Vector3 newRotation = Vector3.RotateTowards(turretHead.forward, relativePlayerPosition - turretHead.position, turretRotationSpeed * Time.deltaTime, 0f);
            turretHead.rotation = Quaternion.LookRotation(newRotation);
        }
    }

    public virtual void Shoot()
    {
        if (firePositions[currentFiringPosition] != null)
        {
            nextTimeToFire = enemyStats.stat[StatType.firingSpeed];

            Transform firePos = firePositions[currentFiringPosition];
            GameObject newProjectile = Instantiate(projectilePrefab, firePos.position, firePos.rotation);

            ProjectileScript bulletScript = newProjectile.GetComponent<ProjectileScript>();
            newProjectile.GetComponentInChildren<TrailRenderer>().colorGradient = colorGradient;

            AddStats(bulletScript);
            bulletScript.Launch();
        }
        
        if (currentFiringPosition != firePositions.Count - 1)
        {
            currentFiringPosition++;
        }
        else
        {
            currentFiringPosition = 0;
        }
    }
}