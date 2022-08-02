using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Dictionary<int, List<GameObject>> powerGrid;

    // public List<PowerGrid> currentLevelPowerGrids;

    public void Awake()
    {
        if (LevelManager.instance == null)
        {
            instance = this;
            powerGrid = new Dictionary<int, List<GameObject>>();
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    public void AddToPowerGrid(int powerGridID, GameObject objectToAdd)
    {
        if (powerGrid[powerGridID] != null)
        {
            powerGrid[powerGridID].Add(objectToAdd);
        }
        else
        {
            powerGrid[powerGridID] = new List<GameObject>();
            powerGrid[powerGridID].Add(objectToAdd);
        }
    }

    public void DisablePowerGrid(int powerGridID)
    {
        foreach(GameObject obj in powerGrid[powerGridID])
        {
            if (obj.GetComponent<DoorScript>() != null)
            {
                obj.GetComponent<DoorScript>().Deactivate();
            }
        }
    }
}
