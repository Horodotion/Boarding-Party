using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Enemy enemy;
    public Gun gun;
    public Stats enemyStats;

    void Awake()
    {
        //enemy = Instantiate(enemy);
        //enemy.enemyController = this;
        enemyStats = Instantiate(enemyStats);
        enemyStats.SetStats();
    }

    public void ChangeHealth(int i)
    {
        enemyStats.stat[StatType.health] = (int)MasterManager.ReduceToZero(enemyStats.stat[StatType.health], i);
        if (enemyStats.stat[StatType.health] == 0)
        {
            CommitDie();
        }
    }

    public void CommitDie()
    {
        Destroy(gameObject);
        Debug.Log("Dead");
    }
}
