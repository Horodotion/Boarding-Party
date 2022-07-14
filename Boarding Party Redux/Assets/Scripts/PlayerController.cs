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
    [HideInInspector]public int playerNumber, playerDisplayNumber;
    [HideInInspector] public PlayerState playerState;
    [HideInInspector] public InputActionAsset playerInputs;
    public Gun gun;
    public Ability genericAbility;
    public Ability movementAbility;

    [Header("Model and Art Objects")]
    //Variables for components on the player Object
    public GameObject playerModel;
    [HideInInspector] public CharacterController ourPlayerController;
    [HideInInspector] public Animator animator;
    [HideInInspector] public GameObject firePosition;
    [HideInInspector] public Collider playerCollider;

    //Variables for the player animations
    private string animUpDown = "UpDown";
    private string animLeftRight = "LeftRight";

    //Variables for movement
    [HideInInspector] public Vector2 moveAxis;
    [HideInInspector] public Vector2 lookAxis;
    [HideInInspector] public bool dead;

    void Awake()
    {
        //Setting up a new instance of scripts to not run into errors with other players
        playerInputs = GetComponent<PlayerInput>().actions;

        //playerStats = Instantiate(playerStats);
        PlayerSpawned();

        DontDestroyOnLoad(gameObject);

        for (int i = 0; i < GeneralManager.playerList.Length; i++)
        {
            if (GeneralManager.playerList[i] == null)
            {
                GeneralManager.playerList[i] = this;
                playerNumber = i;
                playerDisplayNumber = i+1;
                break;
            }
        }


    }

    void Enable()
    {

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
            if (lookDirection == Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(moveDirection, Vector3.up);

                float forwardDirection = Vector3.Dot(moveDirection.normalized, transform.forward);
                animator.SetFloat(animUpDown, forwardDirection, 0.1f, Time.deltaTime);
                animator.SetFloat(animLeftRight, 0, 0.1f, Time.deltaTime);
            }
            else
            {
                float forwardDirection = Vector3.Dot(moveDirection.normalized, transform.forward);
                float sideDirection = Vector3.Dot(moveDirection.normalized, transform.right);
                animator.SetFloat(animUpDown, forwardDirection, 0.1f, Time.deltaTime);
                animator.SetFloat(animLeftRight, sideDirection, 0.1f, Time.deltaTime);
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

        foreach(Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            if (renderer.gameObject.tag == "Player")
            {
                renderer.material = GeneralManager.manager.playerMaterials[playerNumber];
            }
        }
        if (GetComponentInChildren<Image>() != null)
        {
            GetComponentInChildren<Image>().sprite = GeneralManager.manager.playerAuras[playerNumber];
        }
        if (GetComponent<Collider>() != null)
        {
            playerCollider = GetComponent<Collider>();
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

        Debug.Log(playerStats.stat[StatType.health] + " health remaining");
        if (playerStats.stat[StatType.health] <= 0)
        {
            CommitDie();
        }
    }

    public void CommitDie()
    {
        // Destroy(gameObject);
        playerState = PlayerState.dead;
        playerCollider.enabled = false;
        playerModel.SetActive(false);
        dead = true;

        Debug.Log("Dead");
    }

    public void Respawn()
    {
        playerState = PlayerState.inGame;
        playerCollider.enabled = true;
        playerModel.SetActive(true);
        dead = false;

        Debug.Log("I lived bitch");
    }

    public void OnPrimaryButton(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (dead == false)
            {
                genericAbility.Activate(Time.deltaTime);
            }
            else
            {
                Respawn();
            }
        }
    }

    public void OnSecondaryButton(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            genericAbility.Activate();
        }
    }

    public void OnTertiaryButton(InputAction.CallbackContext ctx)
    {     

    }

    public void OnUtilityButton(InputAction.CallbackContext ctx)
    {

    }

    public void OnStartButton(InputAction.CallbackContext ctx)
    {
        Debug.Log("start");
    }

    public void OnSelectButton(InputAction.CallbackContext ctx)
    {
        Debug.Log("spawned");
        PlayerSpawned();
    }

    public void OnAnyAction(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && dead == true)
        {
            Respawn();
        }
    }

    public void OnMovementAxis(InputAction.CallbackContext ctx) => moveAxis = new Vector2(ctx.ReadValue<Vector2>().x, ctx.ReadValue<Vector2>().y);
    public void OnLookAxis(InputAction.CallbackContext ctx) => lookAxis = new Vector2(ctx.ReadValue<Vector2>().x, ctx.ReadValue<Vector2>().y);

    void OnPlayerJoined()
    {
        
    }
}
