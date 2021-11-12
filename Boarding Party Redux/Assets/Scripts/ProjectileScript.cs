using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{

    public int damage;
    public int projectileSpeed;
    private Rigidbody rb;
    public Faction hostileFaction;
    public float lifeSpan;
    public float explosionDuration;
    public Gradient gradient;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        lifeSpan -= Time.deltaTime;
        if(lifeSpan <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Launch()
    {
        rb.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);
    }
}
