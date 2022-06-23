using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType
{
    bullet,
    laser,
    explosive,
    grenade,
    other
}

public class ProjectileScript : MonoBehaviour
{
    public Faction hostileFaction;
    public int damage;
    public List<Status> ourStatusEffects;
    public int projectileSpeed;
    [HideInInspector] public Rigidbody rb;
    public float lifeSpan;

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

    void OnTriggerEnter(Collider col)
    {
        //Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "Wall" || col.gameObject.tag == hostileFaction.ToString() + "")
        {
            OnHitEffects(col.gameObject);
            Destroy(gameObject);
        }
    }

    public void Launch()
    {
        rb.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);
    }

    public void OnHitEffects(GameObject hit)
    {
        if (hostileFaction == Faction.Enemy)
        {
            hit.GetComponent<EnemyController>().ChangeHealth(-damage);

            if (ourStatusEffects.Count != 0)
            {
                for (int i = 0; i < ourStatusEffects.Count; i++)
                {
                    Status newStatus = Instantiate(ourStatusEffects[i]);
                    newStatus.ApplyStatusEffectToEnemy(hit.GetComponent<EnemyController>());
                }
            }
        }
        else if (hostileFaction == Faction.Player)
        {
            hit.GetComponent<PlayerController>().ChangeHealth(-damage);

            if (ourStatusEffects.Count != 0)
            {
                for (int i = 0; i < ourStatusEffects.Count; i++)
                {
                    Status newStatus = Instantiate(ourStatusEffects[i]);
                    newStatus.ApplyStatusEffectToPlayer(hit.GetComponent<PlayerController>());
                }
            }
        }
    }
}
