using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles all the player actions like movement, jumping and interacting
/// </summary>
public class PlayerControler : MonoBehaviour
{
    private const float MOVEMENTSPEED_DAMPNER = 0.1f;

    //! Reference to center point and follow point of the player characters
    public GameObject followPoint, centerPoint;

    //! Reference to the 3 player characters
    private List<GameObject> playerCharacters = new List<GameObject>();

    //! Refernce to player interaction script
    private PlayerInteraction playerInteraction;

    //! Refetence to player input (action maps)
    private PlayerInput input;

    //! True average center point of the 3 player characters
    //[HideInInspector]
    public Vector3 playersTrueCenter = Vector3.zero;

    private Vector2 movementVector;
    private bool isJumping = false;
    private bool isInteracting = false;

    [SerializeField]
    private float movementSpeed = 1;

    //! Initialise the 3 player characters
    private void Start()
    {
        playerInteraction = centerPoint.GetComponent<PlayerInteraction>();

        foreach (PlayerCharacterMovement playerCharacter in GetComponentsInChildren<PlayerCharacterMovement>())
        {
            playerCharacters.Add(playerCharacter.gameObject);
        }

        input = GetComponent<PlayerInput>();
    }

    //! Apply move, jump and interact based when no UIscreens/dialogue are active. 
    void FixedUpdate()
    {
        // Handle input action enabling
        HandleInput();

        // Apply Move, Jump Interact
        MovePlayer();
        JumpPlayers();
        InteractWithOther();
    }

    /// <summary>
    /// Enables or Disbales certain input actions based on UI panels being active
    /// and/or dialogue boxes being active
    /// </summary>
    private void HandleInput()
    {
        if (GameManager.Instance.UIIsActive)
        {
            input.actions.Disable();
        }
        else
        {
            input.actions.Enable();
            if (DialogueManager.Instance.dialogueBox.activeSelf)
            {
                input.actions.FindAction("Move").Disable();
                input.actions.FindAction("Jump").Disable();
            }
            else
            {
                input.actions.FindAction("Move").Enable();
                input.actions.FindAction("Jump").Enable();
            }
        }
    }

    /// <summary>
    /// Moves players position on 2 axis (walking)
    /// </summary>
    private void MovePlayer()
    {
        followPoint.transform.position += new Vector3 (movementVector.x, 0, movementVector.y) * movementSpeed;

        // Calculate players true center point for the camera to follow along
        Vector3 playerCenter = Vector3.zero;
        foreach (GameObject player in playerCharacters)
        {
            playerCenter += player.transform.position/3;
        }
        playersTrueCenter = playerCenter;
        playersTrueCenter.y = centerPoint.transform.position.y;
        centerPoint.transform.position = playersTrueCenter;
    }

    /// <summary>
    /// Checks if all the playerCharacters should jump and then calls the Jump() from withtin the playerCharacters 
    /// </summary>
    private void JumpPlayers()
    {
        if (isJumping)
        {
            isJumping = false;
            foreach (GameObject playerCharacter in playerCharacters)
            {
                playerCharacter.GetComponent<PlayerJump>().Jump();
            }
        }
    }

    /// <summary>
    /// Checks if the player should interact with somthing and then calls the interaction from the playerInteraction script
    /// Also goes through dialogue.
    /// </summary>
    private void InteractWithOther()
    {
        if (isInteracting)
        {
            isInteracting = false;

            if (DialogueManager.Instance.dialogueBox.activeSelf)
            {
                DialogueManager.Instance.DisplayNextSentence();
            }
            else
            {
                playerInteraction.InteractWithInteractable();
            }
        }
    }

    /// <summary>
    /// Gets players movement input and turns it into a vector
    /// </summary>
    /// <param name="value"></param>
    public void OnPlayerMove(InputAction.CallbackContext value)
    {
        movementVector = value.ReadValue<Vector2>()* MOVEMENTSPEED_DAMPNER;
    }

    /// <summary>
    /// Gets players jump input and turns it into a boolean
    /// </summary>
    /// <param name="value"></param>
    public void OnPlayerJump(InputAction.CallbackContext value)
    {
        if (value.started)
            isJumping = true;
    }
    
    /// <summary>
    /// Gets players interact input and turns it into a boolean
    /// </summary>
    /// <param name="value"></param>
    public void OnPlayerInteract(InputAction.CallbackContext value)
    {
        if (value.started)
            isInteracting = true;
    }
}
