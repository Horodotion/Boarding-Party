using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Stats enemyStats;
    public NavMeshAgent navAgent;

    void Awake()
    {
        enemyStats = Instantiate(enemyStats);
        enemyStats.SetStats();
    }

    public virtual void ChangeHealth(int i)
    {
        enemyStats.stat[StatType.health] = (int)MasterManager.ReduceToZero(enemyStats.stat[StatType.health], i);
        if (enemyStats.stat[StatType.health] >= 0)
        {
            CommitDie();
        }
    }

    public virtual void CommitDie()
    {
        Destroy(gameObject);
        Debug.Log("Dead");
    }
}
