using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{

    public int damage;
    public int projectileSpeed;
    private Rigidbody rb;
    public Faction hostileFaction;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    public void Launch()
    {
        rb.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);
    }
}
