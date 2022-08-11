using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum Faction
{
    Player,
    Enemy,
    Wall
}

public enum PlayerClass
{
    mobile,
    soldier,
    specops,
    engineer,
    heavy,
    basic
}

public class GeneralManager : MonoBehaviour
{
    public static GeneralManager manager;
    public static PlayerController[] playerList = new PlayerController[4];
    public static int playersAliveInGame;

    [Header("UI Menus")]
    public GameObject pauseMenu;
    public GameObject winScreen;
    public GameObject timerText;
    public GameObject selector;
    public Button currentlySelectedButton;
    public bool buttonPressedForFrame;

    [Header("Game Score")]
    public int score;

    [Header("Player Prefab Content")]
    public Material[] playerMaterials;
    public Sprite[] playerAuras;

    void Awake()
    {
        if (manager != this && manager == null)
        {
            manager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
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

    public void OpenWinScreen()
    {
        winScreen.SetActive(true);

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
        Debug.Log("Level " + SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        winScreen.SetActive(false);
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
