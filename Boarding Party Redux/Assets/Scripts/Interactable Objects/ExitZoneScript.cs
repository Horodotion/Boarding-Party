using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitZoneScript : MonoBehaviour
{
    public List<GameObject> playersInExitZone;


    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && col.gameObject.GetComponent<PlayerController>() != null
        && !playersInExitZone.Contains(col.gameObject) && LevelManager.instance.selfDestructing)
        {
            playersInExitZone.Add(col.gameObject);

            if (playersInExitZone.Count >= GeneralManager.playersAliveInGame)
            {
                GeneralManager.OpenMenu(GeneralManager.winScreen);
            }
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player" && col.gameObject.GetComponent<PlayerController>() != null
        && playersInExitZone.Contains(col.gameObject) && LevelManager.instance.selfDestructing)
        {
            playersInExitZone.Remove(col.gameObject);
        }
    }
}
