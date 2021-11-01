using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    //Variables to the player's stats

    //The plain Stat refers to the stat that will be referenced by the player
    //Base refers to the base stat to return to upon any resetting.
    //Mods refers to the amount to add to the 
    public int moveSpeed = 5;
    public int moveSpeedBase = 5;
    public int moveSpeedMods = 0;

    public int damage = 5;
    public int damageBase = 5;
    public int damageMods = 0;

    public PlayerClass ourClass = PlayerClass.basic;

    public void ChangeClass(PlayerClass newClass)
    {
        ourClass = newClass;
        Debug.Log("Class changed to: " + ourClass.ToString());
    }
}