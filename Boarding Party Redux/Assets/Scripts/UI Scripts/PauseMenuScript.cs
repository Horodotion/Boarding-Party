using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenuScript : MenuScript
{
    [Header("Pause Menu Variables")]
    public static PauseMenuScript instance;
    public Button resumeButton;
    public Button controlsButton;
    public Button exitButton;
    public Button mainMenuButton;

    public GameObject ControlsPanel;

    void Start()
    {
        if (GeneralManager.pauseMenu == null)
        {
            GeneralManager.pauseMenu = this;
        }
        else if (GeneralManager.pauseMenu != this)
        {
            Destroy(this.gameObject);
        }

        resumeButton.onClick.AddListener(GeneralManager.manager.ExitMenus);
        controlsButton.onClick.AddListener(ShowControlsPanel);
        mainMenuButton.onClick.AddListener(GeneralManager.manager.ReturnToMainMenu);

        for (int i = 0; i < GeneralManager.playerList.Length; i++)
        {
            exitButton.onClick.AddListener(() => {GeneralManager.DisconnectPlayer(i);});
        }

        gameObject.SetActive(false);
    }

    public void ShowControlsPanel()
    {
        if (!ControlsPanel.activeInHierarchy)
        {
            ControlsPanel.SetActive(true);
        }
        else
        {
            ControlsPanel.SetActive(false);
        }

    }
}
