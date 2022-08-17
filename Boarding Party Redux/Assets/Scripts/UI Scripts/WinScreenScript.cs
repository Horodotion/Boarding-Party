using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinScreenScript : MenuScript
{
    [Header("Win Menu Variables")]
    public Button nextLevelButton;
    public Button mainMenuButton;
    public List<TMP_Text> playerScoreTextBoxes;

    public GameObject allPlayersSurvivedIcon;

    void Awake()
    {
        if (GeneralManager.winScreen == null)
        {
            GeneralManager.winScreen = this;
        }
        else if (GeneralManager.winScreen != this)
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        nextLevelButton.onClick.AddListener(GeneralManager.manager.LoadNextLevel);
        mainMenuButton.onClick.AddListener(GeneralManager.manager.ReturnToMainMenu);
    }
}
