using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum PlayerState
{
    inGame,
    inMenu,
    dead,
    idle
}

public class PlayerController : MonoBehaviour
{
    [Header("Stats and Abilities")]

    //The plain Stat (the first number) refers to the stat that will be referenced by the player
    //Base (The second number) refers to the base stat to return to upon any resetting.
    //Max refers to the amount to maximum that a stat can be
    public Stats playerStats;

    //Variables to reference other scripts or controls
    public int playerNumber, playerDisplayNumber, deathCount = 0, respawnCost;
    public PlayerState playerState;
    [HideInInspector] public InputActionAsset playerInputs;
    public Gun gun;
    public Ability genericAbility;
    public Ability movementAbility;

    [Header("Model and Art Objects")]
    //Variables for components on the player Object
    public GameObject playerModel;
    public PlayerObject ourPlayerObject;
    [HideInInspector] public CharacterController ourPlayerController;
    [HideInInspector] public Animator animator;
    [HideInInspector] public GameObject firePosition;
    [HideInInspector] public Collider playerCollider;

    //Variables for the player animations
    [HideInInspector] public string animUpDown = "UpDown";
    [HideInInspector] public string animLeftRight = "LeftRight";

    //Variables for movement
    [HideInInspector] public Vector2 moveAxis;
    [HideInInspector] public Vector2 lookAxis;
    [HideInInspector] public bool dead;

    [Header("UI Components")]
    public PlayerUIScript ourUIScript;
    public List<InteractableObject> interactableObjectList;


    void Awake()
    {
        //Setting up a new instance of scripts to not run into errors with other players
        playerInputs = GetComponent<PlayerInput>().actions;

        //playerStats = Instantiate(playerStats);

        DontDestroyOnLoad(gameObject);

        bool gmExists = false;

        for (int i = 0; i < GeneralManager.playerList.Length; i++)
        {
            if (GeneralManager.playerList[i] == null)
            {
                GeneralManager.playerList[i] = this;
                GeneralManager.playersAliveInGame++;
                playerNumber = i;
                playerDisplayNumber = i+1;
                PlayerSpawned();

                gmExists = true;
                break;
            }
        }

        if (!gmExists)
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        if (PlayerCamera.instance != null)
        {
            PlayerCamera.instance.MovePlayerToSpawn(this);
        }
    }

    void FixedUpdate()
    {
        switch (playerState)
        {
            case (PlayerState.inGame):
                Movement();
                Cooldowns();
                break;

            case (PlayerState.inMenu):
                break;

            case (PlayerState.idle):
                Cooldowns();
                break;

            case (PlayerState.dead):
                break;
        }

    }

    public void Movement()
    {
        Vector3 moveDirection = new Vector3(moveAxis.x, 0, moveAxis.y);
        Vector3 lookDirection = new Vector3(lookAxis.x, 0, lookAxis.y);

        //This part controls the player's movement and 
        if (moveDirection != Vector3.zero)
        {
            //Sets the Vector3 to have a magnetude of 1 and then scales it to the time, with an adjustable speed function
            // moveDirection = new Vector3(moveDirection.x, 0, moveDirection.z).normalized * Time.deltaTime * moveSpeed[0];
            moveDirection = new Vector3(moveDirection.x, 0, moveDirection.z).normalized * Time.deltaTime * playerStats.stat[StatType.speed];

            ourPlayerController.Move(moveDirection);

            //Setting the animation correct animation state of the player.
            //If the left stick is not in use, setting the rotation to the movedirection.
            Vector3 animatorDirection = GetDirectionForAnimator(moveDirection);

            if (lookDirection == Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(moveDirection, Vector3.up);

                animator.SetFloat(animUpDown, animatorDirection.z, 0.1f, Time.deltaTime);
                animator.SetFloat(animLeftRight, 0, 0.1f, Time.deltaTime);
            }
            else
            {
                // float forwardDirection = Vector3.Dot(moveDirection.normalized, transform.forward);
                // float sideDirection = Vector3.Dot(moveDirection.normalized, transform.right);
                animator.SetFloat(animUpDown, animatorDirection.z, 0.1f, Time.deltaTime);
                animator.SetFloat(animLeftRight, animatorDirection.x, 0.1f, Time.deltaTime);
            }
        }
        //If no movement is detected, set the animation to the idle
        else
        {
            animator.SetFloat(animUpDown, 0, 0.1f, Time.deltaTime);
            animator.SetFloat(animLeftRight, 0, 0.1f, Time.deltaTime);
        }

        //Setting the rotation of the player while the movement is not being used
        if (lookDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            
            if (gun.nextTimeToFire == 0)
            {
                gun.Fire(this);
            }
        }
    }

    public void Cooldowns()
    {
        if (gun.nextTimeToFire != 0)
        {
            gun.nextTimeToFire = GeneralManager.ReduceToZeroByTime(gun.nextTimeToFire);
        }

        if (genericAbility != null)
        {
            genericAbility.ReduceCooldown(Time.deltaTime);
        }

        if (movementAbility != null)
        {
            genericAbility.ReduceCooldown(Time.deltaTime);
        }
    }

    public Ability InitializeAbility(Ability ability)
    {
        Ability newAbility = Instantiate(ability);
        newAbility.player = this;
        return newAbility;
    }

    public void ChangeHealth(int i)
    {
        playerStats.stat[StatType.health] = Mathf.Clamp(playerStats.stat[StatType.health] + i, 0, Mathf.Infinity);

        if (ourUIScript != null)
        {
            ourUIScript.UpdateHealth();
        }

        
        if (playerStats.stat[StatType.health] <= 0 && !dead)
        {
            CommitDie();
        }
    }

    public void PlayerSpawned()
    {
        //Getting a reference to the pieces of the game object and children
        ourPlayerController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        firePosition = GetComponentInChildren<SphereCollider>().gameObject;

        playerStats = Instantiate(playerStats);
        playerStats.SetStats();
        dead = false;

        if (genericAbility != null)
        {
            genericAbility = InitializeAbility(genericAbility);
        }

        if (movementAbility != null)
        {
            movementAbility = InitializeAbility(movementAbility);
        }

        if (gun != null)
        {
            gun = Instantiate(gun);
        }

        ourPlayerObject = GeneralManager.manager.playerObjects[playerNumber];
        ourUIScript = GeneralManager.manager.playerUIObjects[playerNumber];
        ourUIScript.InitializePlayerUI(this);

        foreach(Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            if (renderer.gameObject.tag == "Player")
            {
                renderer.material = ourPlayerObject.playerMaterial;
            }
        }
        if (GetComponentInChildren<Image>() != null)
        {
            GetComponentInChildren<Image>().sprite = ourPlayerObject.playerAura;
        }
        if (GetComponent<Collider>() != null)
        {
            playerCollider = GetComponent<Collider>();
        }

        if (GeneralManager.gameState == GameState.inMenu || GeneralManager.gameState == GameState.inMainMenu)
        {
            playerState = PlayerState.inMenu;
            GeneralManager.previousPlayerState[playerNumber] = PlayerState.inMenu;
        }

        if (PlayerCamera.instance != null)
        {
            PlayerCamera.instance.MovePlayerToSpawn(this);
        }
    }

    public void CommitDie()
    {
        playerState = PlayerState.dead;
        GeneralManager.playersAliveInGame--;
        deathCount++;
        respawnCost = 100 * deathCount;
        playerCollider.enabled = false;
        playerModel.SetActive(false);
        dead = true;
    }

    public void Respawn()
    {
        if (GeneralManager.manager.score >= respawnCost)
        {
            GeneralManager.manager.score -= respawnCost;
            UpdateScore(-respawnCost);
            GeneralManager.playersAliveInGame++;

            playerState = PlayerState.inGame;
            playerCollider.enabled = true;
            playerStats.ResetStat(StatType.health);
            playerModel.SetActive(true);
            dead = false;

            if (PlayerCamera.instance != null)
            {
                PlayerCamera.instance.MovePlayerToSpawn(this);
            }

            Debug.Log("I lived bitch");
        }
        else
        {
            Debug.Log("Come back when you're a little mmmmm... Richer?");
        }
    }

    public void UpdateScore(int scoreToRecieve)
    {
        playerStats.stat[StatType.score] += scoreToRecieve;
        ourUIScript.UpdateScore();
        Debug.Log(scoreToRecieve);
    }

    public void DisconnectPlayer()
    {

    }

    public void OnPrimaryButton(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && playerState != PlayerState.inMenu)
        {
            if (dead == false)
            {
                genericAbility.Activate();
            }
            else
            {
                Respawn();
            }
        }
    }

    public void OnSecondaryButton(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && playerState != PlayerState.inMenu)
        {
            genericAbility.Activate();
        }
    }

    public void OnTertiaryButton(InputAction.CallbackContext ctx)
    {     

    }

    public void OnUtilityButton(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (playerState == PlayerState.inMenu && !GeneralManager.manager.buttonPressedForFrame)
            {
                GeneralManager.manager.currentlySelectedButton.onClick.Invoke();
            }
            else if (playerState == PlayerState.inGame && interactableObjectList.Count != 0)
            {
                if (interactableObjectList.Count == 1)
                {
                    interactableObjectList[0].Interact(this);
                }
                else
                {
                    for(int i = 0; i > interactableObjectList.Count; i++)
                    {
                        
                    }
                }
            }
        }
    }

    public void OnStartButton(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (playerState != PlayerState.inMenu)
            {
                GeneralManager.OpenMenu(GeneralManager.pauseMenu);
            }
            else if (GeneralManager.gameState != GameState.inMainMenu)
            {
                GeneralManager.manager.ExitMenus();
            }
        }
    }

    public void OnSelectButton(InputAction.CallbackContext ctx)
    {
        Debug.Log("spawned");
        PlayerSpawned();
    }

    public void OnAnyAction(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && dead == true && playerState != PlayerState.inMenu)
        {
            Respawn();
        }
    }

    public void OnMovementAxis(InputAction.CallbackContext ctx)
    {
        if (playerState != PlayerState.idle)
        {
            moveAxis = new Vector2(ctx.ReadValue<Vector2>().x, ctx.ReadValue<Vector2>().y);

            if (playerState == PlayerState.inMenu && !GeneralManager.manager.buttonPressedForFrame)
            {
                GeneralManager.manager.CycleThroughMenu((int)moveAxis.y);
            }
        }
    }

    public void OnLookAxis(InputAction.CallbackContext ctx)
    {
        lookAxis = new Vector2(ctx.ReadValue<Vector2>().x, ctx.ReadValue<Vector2>().y);
    }

    public void OnPlayerJoined()
    {
        
    }

    public Vector3 GetDirectionForAnimator(Vector3 directionToLook)
    {
        Vector3 vectorToReturn = Vector3.zero;

        vectorToReturn.z = Mathf.Clamp(Vector3.Dot(directionToLook.normalized, transform.forward), -1, 1);
        vectorToReturn.x = Mathf.Clamp(Vector3.Dot(directionToLook.normalized, transform.right), -1, 1);

        return vectorToReturn;
    }
}
