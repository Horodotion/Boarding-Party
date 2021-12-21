using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : ScriptableObject
{
    public EnemyController enemyController;

    public virtual void EnemyAwake()
    {
        enemyController.enemy = Instantiate(this);
    }
}
