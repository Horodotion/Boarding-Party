using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : EnemyController
{
    public int explosiveDamage;
    public float explosiveRange;
    // public float damage;

    public override void Aggro()
    {
        if (!DetectPlayers() || targettedPlayer == null)
        {
            currentState = EnemyState.searching;
            lastDetectedPlayer = lastDetectedPlayerDuration;
        }
        else
        {
            navAgent.SetDestination(targettedPlayer.transform.position);
        }
    }

    public override void CommitDie()
    {
        Destroy(gameObject);
        Debug.Log("Dead");
    }
}
