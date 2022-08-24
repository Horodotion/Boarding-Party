using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera instance;
    public Vector3 cameraOffsetFromPlayers;
    public Vector3[] playerSpawnOffsets = new Vector3[4];

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        foreach (PlayerController player in GeneralManager.playerList)
        {
            if (player != null)
            {
                MovePlayerToSpawn(player);
            }
        }
    }

    void FixedUpdate()
    {
        float totalPlayers = 0;
        float totalX = 0;
        float totalZ = 0;

        foreach (PlayerController player in GeneralManager.playerList)
        {
            if (player != null && !player.dead)
            {
                totalPlayers++;
                totalX += player.gameObject.transform.position.x;
                totalZ += player.gameObject.transform.position.z;
            }
        }

        if (totalPlayers >= 1)
        {
            transform.position = new Vector3(totalX / totalPlayers, transform.position.y, totalZ / totalPlayers) + cameraOffsetFromPlayers;
        }
    }

    public void MovePlayerToSpawn(PlayerController player)
    {
        player.gameObject.transform.position = transform.position + playerSpawnOffsets[player.playerNumber];
    }
}
