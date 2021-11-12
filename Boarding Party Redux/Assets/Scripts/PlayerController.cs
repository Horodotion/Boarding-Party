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
    public PlayerState playerState;
    public InputActionAsset playerInputs;
    public Gun gunObject;
    private Gun gun;

    //Variables for components on the player Object
    private CharacterController ourPlayerController;
    private Animator animator;
    public GameObject firePosition;

    //Variables for the player animations
    private string animUpDown = "UpDown";
    private string animLeftRight = "LeftRight";

    //Variables for movement
    private Vector2 moveAxis;
    private Vector2 lookAxis;


    //The plain Stat refers to the stat that will be referenced by the player
    //Base refers to the base stat to return to upon any resetting.
    //Mods refers to the amount to add to the 
    public int[] moveSpeed = new int[3] {5, 5, 0};
    public int[] damage = new int[3] {10, 10, 0};

    public PlayerClass ourClass = PlayerClass.basic;

    void Awake()
    {
        //Setting up a new instance of scripts to not run into errors with other players
        playerInputs = GetComponent<PlayerInput>().actions;
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
                
                if (gun.nextTimeToFire != 0)
                {
                    gun.nextTimeToFire = ReduceToZeroByTime(gun.nextTimeToFire);
                }
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
            moveDirection = new Vector3(moveDirection.x, 0, moveDirection.z).normalized * Time.deltaTime * moveSpeed[0];

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

    public float ReduceToZeroByTime(float i)
    {
        i = Mathf.Clamp(i - Time.deltaTime, 0, Mathf.Infinity);
        return i;
    }

    public void PlayerSpawned()
    {
        //Getting a reference to the pieces of the game object and children
        ourPlayerController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        firePosition = GetComponentInChildren<SphereCollider>().gameObject;

        Gun newGun = Instantiate(gunObject);
        gun = newGun;
    }



    public void OnPrimaryButton(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Debug.Log("a");
        }
    }

    public void OnSecondaryButton(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            gun.Fire(this);
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
