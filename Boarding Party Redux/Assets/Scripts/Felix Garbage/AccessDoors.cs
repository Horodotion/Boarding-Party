using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessDoors : MonoBehaviour
{
    public bool isIn;
    public GameObject door;

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            door.gameObject.SetActive(false);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            door.gameObject.SetActive(true);
        }
    }
}

