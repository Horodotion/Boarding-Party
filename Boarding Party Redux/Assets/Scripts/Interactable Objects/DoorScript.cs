using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
    energyField,
    blastDoor,
    other
}

public class DoorScript : MonoBehaviour
{
    [Header("Shared Door Variables")]
    public DoorType doorType;

    [Header("Energy Field Variables")]
    public int powerGridID;

    [Header("Blast Door Variables")]
    public bool keyRequired;


    void Start()
    {
        switch (doorType)
        {
            case (DoorType.energyField):
                InitializeEnergyGrid();
                break;

            case (DoorType.blastDoor):
                InitializeBlastDoor();
                break;
        }
    }

    public void InitializeEnergyGrid()
    {
        if (LevelManager.instance != null)
        {
            LevelManager.instance.AddToPowerGrid(powerGridID, gameObject);
        }
        else
        {
            Debug.Log("No Level Manager");
        }
    }

    public void InitializeBlastDoor()
    {

    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
