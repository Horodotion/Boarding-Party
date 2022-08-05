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
    public List<int> powerGridID;

    [Header("Blast Door Variables")]
    public bool keyRequired;
    public bool openDoor = false;
    public Animator doorAnim;
    public Collider doorTrigger;
    private string openAnim = "Open";
    private string closeAnim = "Close";

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

            default:
                break;
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && col.gameObject.GetComponent<PlayerController>() != null && !openDoor)
        {
            openDoor = true;
            doorAnim.SetTrigger(openAnim);
        }
    }

    public void InitializeEnergyGrid()
    {
        if (LevelManager.instance != null)
        {
            foreach (int i in powerGridID)
            {
                LevelManager.instance.AddToPowerGrid(i, gameObject);
            }
        }
        else
        {
            Debug.Log("No Level Manager");
        }
    }

    public void InitializeBlastDoor()
    {
        if (GetComponent<Animator>() != null)
        {
            doorAnim = GetComponent<Animator>();
        }
        foreach (Collider col in GetComponents<Collider>())
        {
            if (col.isTrigger)
            {
                doorTrigger = col;
                break;
            }
        }
    }

    public void Deactivate(int powerGridIDDeactivated)
    {
        switch (doorType)
        {
            case (DoorType.energyField):
                powerGridID.Remove(powerGridIDDeactivated);
                if (powerGridID.Count <= 0)
                {
                    gameObject.SetActive(false);
                }
                break;

            case (DoorType.blastDoor):
            default:    
                break;
        }
    }

    public void OpenAndCloseDoor()
    {

    }
}
