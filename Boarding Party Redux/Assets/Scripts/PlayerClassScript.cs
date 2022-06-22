using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClassScript : ScriptableObject
{
    public PlayerController player;
    public Gun gun;
    public Ability genericAbility;
    public Dictionary<StatType, int> stat = new Dictionary<StatType, int>
    {
        {StatType.speed, 5},
        {StatType.health, 100}, 
        {StatType.damage, 0}
    };
    
    public Dictionary<StatType, int> baseStats = new Dictionary<StatType, int>
    {
        {StatType.speed, 5},
        {StatType.health, 100}, 
        {StatType.damage, 0}
    };

    public Dictionary<StatusType, List<Status>> StatusLists = new Dictionary<StatusType, List<Status>>
    {

    };

    public void ClassStartUp(PlayerController ourPlayer)
    {
        player = ourPlayer;
        gun = Instantiate(player.gun);
        genericAbility = Instantiate(player.genericAbility);
    }

    public void ClassUpdate()
    {
        Cooldowns();
    }

    public void Cooldowns()
    {
        if (gun.nextTimeToFire != 0)
        {
            gun.nextTimeToFire = MasterManager.ReduceToZero(gun.nextTimeToFire, Time.deltaTime);
        }

        genericAbility.ReduceCooldown(Time.deltaTime, player);
    }
}