using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Dictionary<int, List<GameObject>> powerGrid;

    public float selfDestructTimer;
    public float amountOfTimeBeforeSelfDestruct;
    public bool selfDestructing = false;

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

    public void FixedUpdate()
    {
        if (selfDestructing)
        {
            if (selfDestructTimer >= 0)
            {
                selfDestructTimer -= Time.deltaTime;
            }
            else
            {

            }
        }
    }

    public void AddToPowerGrid(int powerGridID, GameObject objectToAdd)
    {
        if (powerGrid.ContainsKey(powerGridID))
        {
            powerGrid[powerGridID].Add(objectToAdd);
        }
        else
        {
            powerGrid.Add(powerGridID, new List<GameObject>());
            powerGrid[powerGridID].Add(objectToAdd);
        }
    }

    public void DisablePowerGrid(int powerGridID)
    {
        foreach(GameObject obj in powerGrid[powerGridID])
        {
            if (obj.GetComponent<DoorScript>() != null)
            {
                obj.GetComponent<DoorScript>().Deactivate(powerGridID);
            }
        }
    }

    public void BeginSelfDestructSequence()
    {
        selfDestructing = true;
        selfDestructTimer = amountOfTimeBeforeSelfDestruct;
    }
}
