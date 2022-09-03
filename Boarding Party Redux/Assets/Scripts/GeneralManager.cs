using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum Faction
{
    Player,
    Enemy,
    Wall
}

public enum GameState
{
    inGame,
    inMenu,
    inMainMenu,
    other
}

[System.Serializable]
public class PlayerObject
{
    public Material playerMaterial;
    public Sprite playerAura;
}

public class GeneralManager : MonoBehaviour
{
    public static GeneralManager manager;
    public static PlayerController[] playerList = new PlayerController[4];
    public static PlayerState[] previousPlayerState = new PlayerState[4];
    public static int playersAliveInGame;
    public static GameState gameState = GameState.inGame;

    [Header("UI Menus")]
    public GameObject inGameUI;
    public static PauseMenuScript pauseMenu;
    public static WinScreenScript winScreen;
    public static HubMenuScript hubMenu;
    public TMP_Text timerText;
    public GameObject selector;

    public List<Button> currentMenuObjects;
    public Button currentlySelectedButton;
    public int currentlySelectedButtonID;
    public bool buttonPressedForFrame;

    [Header("Game Score")]
    public int score;
    public TMP_Text scoreCounter;

    [Header("Player Prefab Content")]
    public PlayerObject[] playerObjects;
    public PlayerUIScript[] playerUIObjects;

    void Awake()
    {
        if (manager == null)
        {
            manager = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (manager != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (buttonPressedForFrame == true)
        {
            buttonPressedForFrame = false;
        }
    }

    void OneEnable()
    {
        InputSystem.onDeviceChange +=(device, change) =>
            {
                switch (change)
                {
                    case InputDeviceChange.Added:
                        Debug.Log("Device added: " + device);
                        break;
                    case InputDeviceChange.Removed:
                        Debug.Log("Device removed: " + device);
                        break;
                    case InputDeviceChange.ConfigurationChanged:
                        Debug.Log("Device configuration changed: " + device);
                        break;
                }
            };
    }

    public void CycleThroughMenu(int direction)
    {
        buttonPressedForFrame = true;

        if (currentMenuObjects != null)
        {
            if (direction < 0)
            {
                currentlySelectedButtonID++;

                if (currentlySelectedButtonID > currentMenuObjects.Count - 1)
                {
                    currentlySelectedButtonID = 0;
                }
            }
            else if (direction > 0)
            {
                currentlySelectedButtonID--;

                if (currentlySelectedButtonID < 0)
                {
                    currentlySelectedButtonID = currentMenuObjects.Count - 1;
                }
            }

            if (currentMenuObjects[currentlySelectedButtonID] != null)
            {
                currentlySelectedButton = currentMenuObjects[currentlySelectedButtonID];
                selector.transform.position = currentMenuObjects[currentlySelectedButtonID].gameObject.transform.position;
                // Debug.Log("Current Button: " + currentlySelectedButtonID);
            }
            else
            {
                Debug.Log("Menu Object is null");
            }
        }
        else
        {
            Debug.Log("No Menu");
        }
    }

    public void SetUpMenu(List<Button> menuObjects)
    {
        if (inGameUI != null)
        {
            inGameUI.SetActive(false);
        }

        Time.timeScale = 0;
        currentMenuObjects = menuObjects;
        currentlySelectedButtonID = 0;
        CycleThroughMenu(0);

        foreach(PlayerController player in playerList)
        {
            if (player != null)
            {
                previousPlayerState[player.playerNumber] = player.playerState;
                player.playerState = PlayerState.inMenu;
            }
        }
    }

    public void ExitMenus()
    {
        gameState = GameState.inGame;

        if (inGameUI != null)
        {
            inGameUI.SetActive(true);
        }
        Time.timeScale = 1;

        if (winScreen != null && winScreen.gameObject.activeInHierarchy)
        {
            winScreen.gameObject.SetActive(false);
            winScreen.allPlayersSurvivedIcon.SetActive(false);
        }
        if (pauseMenu != null && pauseMenu.gameObject.activeInHierarchy)
        {
            pauseMenu.gameObject.SetActive(false);
            pauseMenu.ControlsPanel.SetActive(false);
        }

        foreach(PlayerController player in playerList)
        {
            if (player != null)
            {
                if (previousPlayerState[player.playerNumber] != PlayerState.inMenu)
                {
                    player.playerState = previousPlayerState[player.playerNumber];
                }
                else
                {
                    player.playerState = PlayerState.inGame;
                }
            }
        }
    }

    public void ReturnToMainMenu()
    {
        ExitMenus();
        SceneManager.LoadScene(0);
        foreach(PlayerController player in playerList)
        {
            if (player != null)
            {
                player.playerState = PlayerState.inMenu;
            }
        }
    }

    public void LoadNextLevel()
    {
        Debug.Log("Level to Load " + (SceneManager.GetActiveScene().buildIndex + 1));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
        if (timerText != null)
        {
            timerText.gameObject.SetActive(false);
        }

        foreach(PlayerController player in playerList)
        {
            if (player != null)
            {
                player.playerState = PlayerState.inGame;
            }
        }

        ExitMenus();
    }

    public void SetTimerText(float i)
    {
        timerText.text = "" + i;
    }

    public static void OpenMenu(MenuScript menu)
    {
        if (menu != null)
        {
            Time.timeScale = 0;

            if (GeneralManager.manager.inGameUI != null)
            {
                GeneralManager.manager.inGameUI.SetActive(false);
            }

            gameState = GameState.inMenu;
            menu.Activate();

            foreach(PlayerController player in playerList)
            {
                if (player != null)
                {
                    previousPlayerState[player.playerNumber] = player.playerState;
                    player.playerState = PlayerState.inMenu;
                }
            }
        }
        else
        {
            Debug.Log("No Menu");
        }
    }

    public static void DisconnectPlayer(int playerID)
    {

    }

    public static void UpdateScore(int scoreToRecieve)
    {
        manager.score += scoreToRecieve;
        manager.scoreCounter.text = "" + manager.score;

    }

    public static float ReduceToZeroByTime(float numberToReduce)
    {
        numberToReduce = Mathf.Clamp(numberToReduce - Time.deltaTime, 0, Mathf.Infinity);
        return numberToReduce;
    }

    public static float ReduceToZero(float numberToReduce, float reduction)
    {
        numberToReduce = Mathf.Clamp(numberToReduce - reduction, 0, Mathf.Infinity);
        return numberToReduce;
    }
}
