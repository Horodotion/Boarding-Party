using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
    inGame,
    inMenu,
    idle
}

public class PlayerController : MonoBehaviour
{
    //Variables to reference other scripts or controls
    [HideInInspector] public PlayerState playerState;
    [HideInInspector] public InputActionAsset playerInputs;
    public Gun gun;
    public Ability genericAbility;
    public Ability movementAbility;

    //Variables for components on the player Object
    [HideInInspector] public CharacterController ourPlayerController;
    private Animator animator;
    [HideInInspector] public GameObject firePosition;

    //Variables for the player animations
    private string animUpDown = "UpDown";
    private string animLeftRight = "LeftRight";

    //Variables for movement
    [HideInInspector] public Vector2 moveAxis;
    [HideInInspector] public Vector2 lookAxis;


    //The plain Stat (the first number) refers to the stat that will be referenced by the player
    //Base (The second number) refers to the base stat to return to upon any resetting.
    //Max refers to the amount to maximum that a stat can be
    public Stats playerStats;


    //public Dictionary<Stats, int[]> playerStats = new Dictionary<Stats, int[]>();
    public PlayerClass ourClass = PlayerClass.basic;

    void Awake()
    {
        //Setting up a new instance of scripts to not run into errors with other players
        playerInputs = GetComponent<PlayerInput>().actions;

        //playerStats = Instantiate(playerStats);
        PlayerSpawned();

        DontDestroyOnLoad(gameObject);
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
            gun.nextTimeToFire = MasterManager.ReduceToZero(gun.nextTimeToFire, Time.deltaTime);
        }

        if (genericAbility != null)
        {
            genericAbility.ReduceCooldown(Time.deltaTime);
        }

        if (movementAbility != null)
        {

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

        if (genericAbility != null)
        {
            genericAbility = InitializeAbility(genericAbility);
        }

        if (movementAbility != null)
        {
            movementAbility = InitializeAbility(movementAbility);
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
        playerStats.stat[StatType.health] = (int)MasterManager.ReduceToZero(playerStats.stat[StatType.health], i);
        if (playerStats.stat[StatType.health] == 0)
        {
            CommitDie();
        }
    }

    public void CommitDie()
    {
        Destroy(gameObject);
        Debug.Log("Dead");
    }



    public void OnPrimaryButton(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            genericAbility.Activate(Time.deltaTime);
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

    public void OnMovementAxis(InputAction.CallbackContext ctx) => moveAxis = new Vector2(ctx.ReadValue<Vector2>().x, ctx.ReadValue<Vector2>().y);
    public void OnLookAxis(InputAction.CallbackContext ctx) => lookAxis = new Vector2(ctx.ReadValue<Vector2>().x, ctx.ReadValue<Vector2>().y);

    void OnPlayerJoined()
    {
        
    }
}
