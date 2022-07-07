using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : EnemyController
{
    public Transform turretHead;
    public float turretRotationSpeed;
    public GameObject projectilePrefab;
    public Gradient colorGradient;
    [HideInInspector] public List<Transform> firePositions;
    public float firingSpeed;
    public float nextTimeToFire;
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
            nextTimeToFire = MasterManager.ReduceToZero(nextTimeToFire, Time.deltaTime);
        }
    }

    public override void Searching()
    {
        base.Searching();

        if (nextTimeToFire > 0)
        {
            nextTimeToFire = MasterManager.ReduceToZero(nextTimeToFire, Time.deltaTime);
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
            nextTimeToFire = MasterManager.ReduceToZero(nextTimeToFire, Time.deltaTime);
            
            Vector3 relativePlayerPosition = new Vector3(targettedPlayer.transform.position.x, turretHead.position.y, targettedPlayer.transform.position.z);
            Vector3 newRotation = Vector3.RotateTowards(turretHead.forward, relativePlayerPosition - turretHead.position, turretRotationSpeed * Time.deltaTime, 0f);
            turretHead.rotation = Quaternion.LookRotation(newRotation);
        }
    }

    public virtual void Shoot()
    {
        if (firePositions[currentFiringPosition] != null)
        {
            nextTimeToFire = firingSpeed;

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