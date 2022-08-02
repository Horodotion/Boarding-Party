using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : EnemyController
{
    public int powerGridID;


    public override void Awake()
    {

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

    public override void ChangeHealth(int i, PlayerController playerCreditedForKill = null)
    {
        // enemyStats.stat[StatType.health] = Mathf.Clamp(enemyStats.stat[StatType.health] + i, 0, Mathf.Infinity);
        // if (enemyStats.stat[StatType.health] <= 0)
        // {
        //     CommitDie(playerCreditedForKill);
        // }
    }

    public override void CommitDie(PlayerController playerCreditedForKill = null)
    {
        // if (playerCreditedForKill != null)
        // {
        //     playerCreditedForKill.playerStats.stat[StatType.score] += enemyStats.stat[StatType.score];
        //     Debug.Log(playerCreditedForKill.playerStats.stat[StatType.score]);
        // }
        // else
        // {
        //     foreach (PlayerController player in GeneralManager.playerList)
        //     {
        //         player.playerStats.stat[StatType.score] += enemyStats.stat[StatType.score] / 4;
        //     }
        // }
        // GeneralManager.manager.score += (int)enemyStats.stat[StatType.score];


        LevelManager.instance.DisablePowerGrid(powerGridID);


        // Destroy(gameObject);
        Debug.Log("Dead");
    }
}
