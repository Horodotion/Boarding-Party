using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Factions
{
    player,
    enemy,
    wall
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

    void Awake()
    {
        if (master != this && master == null)
        {
            master = this;
            // allGamepads = Gamepad.all;
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
}
