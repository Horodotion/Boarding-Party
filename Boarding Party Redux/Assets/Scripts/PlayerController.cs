using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
    walking,
    inMenu
}

public class PlayerController : MonoBehaviour
{
    //Variables for the player statistics
    public PlayerStats playerStats;

    //Variables for the player Controller
    private CharacterController ourPlayerController;


    //Variables for the player animations
    private Animator animator;
    private string animUpDown = "UpDown";
    private string animLeftRight = "LeftRight";

    //Variables for movement
    private Vector2 moveAxis;
    private Vector2 lookAxis;


    void Awake()
    {
        //Setting up a new instance of scripts to not run into errors with other players
        playerStats = new PlayerStats();
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(moveAxis.x, 0, moveAxis.y);
        Vector3 lookDirection = new Vector3(lookAxis.x, 0, lookAxis.y);

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

    public void SpawnPlayer()
    {
        //Getting a reference to the pieces of the game object and children
        ourPlayerController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
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
            Debug.Log(Gamepad.current);
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
        Debug.Log("select");
    }

    public void OnMovementAxis(InputAction.CallbackContext ctx) => moveAxis = new Vector2(ctx.ReadValue<Vector2>().x, ctx.ReadValue<Vector2>().y);
    public void OnLookAxis(InputAction.CallbackContext ctx) => lookAxis = new Vector2(ctx.ReadValue<Vector2>().x, ctx.ReadValue<Vector2>().y);

    void OnPlayerJoined()
    {
        
    }
}
