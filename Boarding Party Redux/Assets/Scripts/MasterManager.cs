using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

public class MasterManager : MonoBehaviour
{
    public static MasterManager master;
    public static List<GameObject> playerList;


    void Awake()
    {
        if (master != this && master == null)
        {
            master = this;
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

    public static float ReduceToZero(float i, float reduction)
    {
        i = Mathf.Clamp(i - reduction, 0, Mathf.Infinity);
        return i;
    }
}
