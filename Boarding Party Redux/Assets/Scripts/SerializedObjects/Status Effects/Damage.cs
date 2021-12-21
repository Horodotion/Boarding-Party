using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Status/Damage")]
public class Damage : Status
{

    public override void ApplyStatusEffectToPlayer(PlayerController player = null)
    {
        player.ChangeHealth(statusStrength);
    }

    public override void ApplyStatusEffectToEnemy(EnemyController enemy = null)
    {
        enemy.ChangeHealth(statusStrength);
    }

}
