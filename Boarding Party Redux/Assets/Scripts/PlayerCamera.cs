using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        Vector3 newPos = transform.position + playerSpawnOffsets[player.playerNumber];
        // NavMeshHit hit;
        
        if (Physics.Raycast(new Vector3(newPos.x, 1, newPos.z), transform.TransformDirection(Vector3.down), 5f))
        {
            player.gameObject.transform.position = newPos;
        }
        else
        {
            foreach (PlayerController playerController in GeneralManager.playerList)
            {
                if (playerController != null && playerController != player && !playerController.dead)
                {
                    newPos = playerController.gameObject.transform.position;
                    for (int i = 0; i < 4; i++)
                    {
                        if (Physics.Raycast(new Vector3(newPos.x, 1, newPos.z) + playerSpawnOffsets[i], transform.TransformDirection(Vector3.down), 5f))
                        {
                            newPos += playerSpawnOffsets[i];
                            break;
                        }
                    }
                }
            }

            player.gameObject.transform.position = newPos;
            Debug.Log("Bitch");
        }


        // player.gameObject.transform.position = transform.position + playerSpawnOffsets[player.playerNumber];
    }
}
