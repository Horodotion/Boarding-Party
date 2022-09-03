using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HubMenuScript : MenuScript
{
    [Header("Hub Menu Buttons")]
    public Button playGameButton;
    public Button exitGameButton;
    
    public GameObject ControlsPanel;

    void Start()
    {
        if (GeneralManager.hubMenu == null)
        {
            GeneralManager.hubMenu = this;
        }
        else if (GeneralManager.hubMenu != this)
        {
            Destroy(this.gameObject);
        }

        playGameButton.onClick.AddListener(GeneralManager.manager.LoadNextLevel);
        exitGameButton.onClick.AddListener(Application.Quit);

        GeneralManager.OpenMenu(this);
    }

    public override void Activate()
    {
        GeneralManager.manager.selector = selector;
        GeneralManager.manager.SetUpMenu(allMenuButtons);
        GeneralManager.gameState = GameState.inMainMenu;
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

    public void OpenPlayerReadyScreen()
    {
        
    }
}
