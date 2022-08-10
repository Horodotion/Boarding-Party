using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreGeneratorScript : EnemyController
{
    [Header("Generator Variables")]

    public int health;
    public int score;


    public override void Awake()
    {

    }

    public void Start()
    {

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
        GivePoints(score);
        LevelManager.instance.BeginSelfDestructSequence();

        Destroy(this);
        Debug.Log("Dead");
    }
}
