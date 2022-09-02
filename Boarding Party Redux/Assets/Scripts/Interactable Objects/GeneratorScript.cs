using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : EnemyController
{
    [Header("Generator Variables")]
    public int powerGridID;
    public int health;
    public int score;


    public override void Awake()
    {
        if (GetComponent<Animator>() != null)
        {
            enemyAnim = GetComponent<Animator>();
        }
    }

    public void Start()
    {
        if (LevelManager.instance != null)
        {
            LevelManager.instance.AddToPowerGrid(powerGridID, gameObject);
        }
        else
        {
            Debug.Log("No Level Manager");
        }
    }

    public override void FixedUpdate()
    {
            
    }

    public override void ChangeHealth(int i, PlayerController playerCreditedForKill = null)
    {
        health = (int)Mathf.Clamp(health + i, 0, Mathf.Infinity);
        if (health <= 0)
        {
            CommitDie(playerCreditedForKill);
        }
    }

    public override void CommitDie(PlayerController playerCreditedForKill = null)
    {
        GivePoints(score, playerCreditedForKill);
        LevelManager.instance.DisablePowerGrid(powerGridID);
        
        if (enemyAnim != null)
        {
            enemyAnim.SetTrigger("Spin_on");
        }

        Destroy(this);
    }
}
