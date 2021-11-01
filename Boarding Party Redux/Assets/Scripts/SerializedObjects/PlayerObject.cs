using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Object", menuName = "Player")]
public class PlayerObject : ScriptableObject
{
    //Variables for player creation/spawning
    public string playerName;
    public Material playerMaterial;
    public Sprite playerUIImage;
    public Color ourColor;

    //Variables for Stats
    public int health;
    public int speed;

    //Variables for levels and abilities
    public PlayerClass playerClass;
    public int playerLevel;
}