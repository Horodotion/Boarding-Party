using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinScreenScript : MonoBehaviour
{
    [Header("Buttons")]
    public Button nextLevelButton;
    public Button mainMenuButton;
    public List<TMP_Text> playerScoreTextBoxes;
    public List<Button> allMenuButtons;

    [Header("Non-Clickable References")]
    public GameObject selector;
    public GameObject allPlayersSurvivedIcon;

    public void Awake()
    {
        GeneralManager.manager.currentlySelectedButton = allMenuButtons[0];
        nextLevelButton.onClick.AddListener(GeneralManager.manager.LoadNextLevel);
    }
}
