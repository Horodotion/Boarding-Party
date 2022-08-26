using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    idle,
    searching,
    aggro,
    other
}

public class EnemyController : MonoBehaviour
{
    [Header("Stats")]
    public Stats enemyStats;
    public EnemyState currentState;
    public bool dead;

    [HideInInspector] public NavMeshAgent navAgent;
    [HideInInspector] public GameObject targettedPlayer;
    [HideInInspector] public float lastDetectedPlayer;
    [HideInInspector] public float lastDetectedPlayerDuration = 5f;
    [HideInInspector] public float playerDetection = 1f;
    [HideInInspector] public float playerDetectionRate = 0.2f;
    [HideInInspector] public Faction hostileFaction = Faction.Player;
    [HideInInspector] public Animator enemyAnim;


    public virtual void Awake()
    {
        enemyStats = Instantiate(enemyStats);
        enemyStats.SetStats();

        if (GetComponent<NavMeshAgent>() != null)
        {
            navAgent = GetComponent<NavMeshAgent>();
            navAgent.speed = enemyStats.stat[StatType.speed];
        }
    }

    public virtual void FixedUpdate()
    {
        if (playerDetection < 0f)
        {
            playerDetection = GeneralManager.ReduceToZeroByTime(playerDetection);
        }
        else
        {
            playerDetection = playerDetectionRate;
            DetectPlayers();
        }
        

        switch (currentState)
        {
            case EnemyState.idle:
                Idle();
                break;

            case EnemyState.searching:
                Searching();
                break;

            case EnemyState.aggro:
                Aggro();
                break;

            case EnemyState.other:
                break;

        }
    }

    public virtual void Idle()
    {
        if (targettedPlayer != null)
        {
            currentState = EnemyState.aggro;
        }
    }

    public virtual void Searching()
    {
        if (targettedPlayer != null)
        {
            currentState = EnemyState.aggro;
        }
        else
        {
            lastDetectedPlayer = GeneralManager.ReduceToZeroByTime(lastDetectedPlayer);
            if (lastDetectedPlayer <= 0f)
            {
                currentState = EnemyState.idle;
            }
        }
    }

    public virtual void Aggro()
    {
        if (targettedPlayer == null)
        {
            currentState = EnemyState.searching;
            lastDetectedPlayer = lastDetectedPlayerDuration;
        }
        else
        {
            if (GetComponent<NavMeshAgent>() != null)
            {
                navAgent.SetDestination(targettedPlayer.transform.position);
            }
        }
    }

    public virtual void DetectPlayers()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, enemyStats.stat[StatType.detectionRange]); // 10f);
        List<GameObject> players = new List<GameObject>();

        foreach (Collider hit in hitColliders)
        {
            if (hit.gameObject.tag == "Player" && !players.Contains(hit.gameObject))
            {
                RaycastHit raycastHit;
                Vector3 adjustedPosition = new Vector3(transform.position.x, 1f, transform.position.z);
                Physics.Raycast(adjustedPosition, hit.gameObject.transform.position - transform.position, out raycastHit);

                if (hit.gameObject != null && raycastHit.collider != null && raycastHit.collider.gameObject == hit.gameObject)
                {
                    players.Add(hit.gameObject);
                }
            }
        }

        if (players.Count >= 2)
        {
            GameObject closestPlayer = null;
            float closestPlayerDistance = Mathf.Infinity;

            foreach (GameObject player in players)
            {
                float newDistance = Vector3.Distance(transform.position, player.transform.position);

                if (closestPlayerDistance < newDistance || closestPlayer == null)
                {
                    closestPlayer = player;
                    closestPlayerDistance = newDistance;
                }
            }

            targettedPlayer = closestPlayer;
        }
        else if (players.Count == 1)
        {
            targettedPlayer = players[0];
        }
        else if (players.Count == 0)
        {
            if (targettedPlayer != null)
            {
                Debug.Log(gameObject.name);
            }

            targettedPlayer = null;
        }

        return;
    }

    public virtual void ChangeHealth(int i, PlayerController playerCreditedForKill = null)
    {
        enemyStats.stat[StatType.health] = Mathf.Clamp(enemyStats.stat[StatType.health] + i, 0, Mathf.Infinity);
        if (enemyStats.stat[StatType.health] <= 0)
        {
            CommitDie(playerCreditedForKill);
        }
    }

    public virtual void CommitDie(PlayerController playerCreditedForKill = null)
    {
        GivePoints((int)enemyStats.stat[StatType.score], playerCreditedForKill);
        Destroy(gameObject);
        Debug.Log("Dead");
    }

    public virtual void AddStats(ProjectileScript bulletScript = null)
    {
        bulletScript.hostileFaction = hostileFaction;
        bulletScript.damage = (int)enemyStats.stat[StatType.damage];
    }

    public virtual void GivePoints(int scoreToGive, PlayerController playerCreditedForKill = null)
    {
        GeneralManager.UpdateScore(scoreToGive);

        if (playerCreditedForKill != null)
        {
            playerCreditedForKill.UpdateScore(scoreToGive);
        }
        else
        {
            foreach (PlayerController player in GeneralManager.playerList)
            {
                if (player != null)
                {
                    player.UpdateScore(scoreToGive / 4);
                }
            }
        }
    }
}
