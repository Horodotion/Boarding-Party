using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIScript : MonoBehaviour
{

    [Header("Player Components")]
    public PlayerController ourPlayer;
    public Image playerImage;
    public Slider healthbar;
    public TMP_Text grenadeText, keyText, dashText, scoreText;

    [Header("Variables for when there's no player")]
    public Image noPlayerConnected;

    public void InitializePlayerUI(PlayerController player)
    {
        ourPlayer = player;
        healthbar.maxValue = ourPlayer.playerStats.maxStat[StatType.health];
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        healthbar.value = ourPlayer.playerStats.stat[StatType.health];
    }

    public void UpdateScore()
    {
        scoreText.text = "" + ourPlayer.playerStats.stat[StatType.score];
    }
}
