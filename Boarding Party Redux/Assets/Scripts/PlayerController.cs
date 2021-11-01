using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Variables for the new input system
    //Listed out as Input actions to make further code more readable
    //private Gamepad gamepad;
    private PlayerControlScheme controls;
    private InputAction primaryButton;
    private InputAction secondaryButton;
    private InputAction tertiaryButton;
    private InputAction utilityButton;
    private InputAction startButton;
    private InputAction selectButton;
    private InputAction movementAxis;
    private InputAction lookAxis;

    //Variables for the player statistics
    public PlayerStats playerStats;

    //Variables for the player Controller
    private CharacterController ourPlayerController;


    //Variables for the player animations
    private Animator animator;
    private string animUpDown = "UpDown";
    private string animLeftRight = "LeftRight";


    void Awake()
    {
        //Setting up a new instance of scripts to not run into errors with other players
        controls = new PlayerControlScheme();
        playerStats = new PlayerStats();

        //Setting up the naming convention of the buttons to be more readable throughout scripts
        primaryButton = controls.PlayerControls.primaryButton;
        secondaryButton = controls.PlayerControls.secondaryButton;
        tertiaryButton = controls.PlayerControls.tertiaryButton;
        utilityButton = controls.PlayerControls.utilityButton;
        startButton = controls.PlayerControls.startButton;
        selectButton = controls.PlayerControls.selectButton;
        movementAxis = controls.PlayerControls.movementAxis;
        lookAxis = controls.PlayerControls.lookAxis;

        //Getting a reference to the pieces of the game object and children
        ourPlayerController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    void OnEnable()
    {
        //Enabling the basic controls for the player
        controls.PlayerControls.Enable();

        //Setting up the functions that will be called when using a button
        //Axises don't need to be called this way since they are readonly
        primaryButton.performed += PrimaryButton;
        secondaryButton.performed += SecondaryButton;
        tertiaryButton.performed += TertiaryButton;
        utilityButton.performed += UtilityButton;
        startButton.performed += StartButton;
        selectButton.performed += SelectButton;

    }

    void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(movementAxis.ReadValue<Vector2>().x, 0, movementAxis.ReadValue<Vector2>().y);
        Vector3 lookDirection = new Vector3(lookAxis.ReadValue<Vector2>().x, 0, lookAxis.ReadValue<Vector2>().y);

        //This part controls the player's movement and 
        if (moveDirection != Vector3.zero)
        {
            //Sets the Vector3 to have a magnetude of 1 and then scales it to the time, with an adjustable speed function
            moveDirection = new Vector3(moveDirection.x, 0, moveDirection.z).normalized * Time.deltaTime * playerStats.moveSpeed;

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
        }

    }

    public void PrimaryButton(InputAction.CallbackContext ctx)
    {
        Debug.Log("a");
    }

    public void SecondaryButton(InputAction.CallbackContext ctx)
    {

        Debug.Log(Gamepad.all);
    }

    public void TertiaryButton(InputAction.CallbackContext ctx)
    {     

    }

    public void UtilityButton(InputAction.CallbackContext ctx)
    {

    }

    public void StartButton(InputAction.CallbackContext ctx)
    {
        Debug.Log("start");
    }

    public void SelectButton(InputAction.CallbackContext ctx)
    {
        Debug.Log("select");
        playerStats.ChangeClass(PlayerClass.specops);
    }

    void OnPlayerJoined()
    {
        
    }
}
