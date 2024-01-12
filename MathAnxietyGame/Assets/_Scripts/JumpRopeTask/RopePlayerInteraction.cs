using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This scripts controls all the interaction between the jump rope and the player
/// </summary>
public class RopePlayerInteraction : MonoBehaviour
{
    private JumpRopeRotation jumpRotation;
    [SerializeField]
    private int swingsTillVictory;
    private bool isCompleted = false;
    private bool startSwinging = false;

    // Start is called before the first frame update
    void Start()
    {
        jumpRotation = GetComponent<JumpRopeRotation>();
    }

    void FixedUpdate()
    {
        CheckJumpRopeSucces();

        // Activate the rope swinging when the player is nearby
        if (startSwinging &! isCompleted)
        {
            jumpRotation.StartRotating();
            startSwinging = false;
        }
    }

    /// <summary>
    /// Stops the rope because of failure
    /// </summary>
    public void JumpRopeFailed()
    {
        if (!isCompleted)
        {
            // Reset jump rope
            jumpRotation.StopRotation();
            Debug.Log("You hit the Rope, try again!");
        }
    }

    /// <summary>
    /// Checks if the rope did enough swings and then stops the rope because of completion/succes
    /// </summary>
    private void CheckJumpRopeSucces()
    {
        if (jumpRotation.amountOfSwings >= swingsTillVictory)
        {
            // Stop jump rope
            jumpRotation.StopRotation();
            isCompleted = true;
            Debug.Log("You win!!! Great job!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerCharacterMovement>())
        {
            startSwinging = true;
        }
    }
}
