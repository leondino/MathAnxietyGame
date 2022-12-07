using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles that this player character follows the center point smoothly 
/// </summary>
public class PlayerCharacterMovement : MonoBehaviour
{
    const float MOVE_AWAY_INFLUENCE = 0.3f;
    const float MIN_CENTER_DISTANCE = 2f;

    //! Reference to center point of the player characters
    private GameObject followPoint;

    [SerializeField] [Range(0, 1)]
    private float followSpeed = 0.5f;

    private Rigidbody rBody;
    private bool applyVelocity = true;
    private bool applyMoveAwayVelocity = false;
    private List<Collider> moveAwayTargets = new List<Collider>();

    void Awake()
    {
        rBody = GetComponent<Rigidbody>();
        followPoint = GetComponentInParent<PlayerControler>().followPoint;
    }

    void FixedUpdate()
    {
        FollowTargetPosition();
    }

    /// <summary>
    /// Makes the character follow the player center point and rotate towards it
    /// </summary>
    private void FollowTargetPosition()
    {
        // Caclucalte movement direction
        Vector3 targetPosition = followPoint.transform.position;
        targetPosition.y = rBody.position.y;
        Vector3 targetVelocity = targetPosition - rBody.position;

        // Check if close to center point
        if (targetVelocity.magnitude > MIN_CENTER_DISTANCE)
            applyVelocity = true;
        else 
            applyVelocity = false;

        // Apply movement
        if (applyVelocity)
        {
            targetVelocity *= followSpeed;
            targetVelocity.y = rBody.velocity.y;
            rBody.velocity = targetVelocity;
        }

        // Apply move away from other characters movement
        if (applyMoveAwayVelocity)
        {
            foreach (Collider moveAwayTarget in moveAwayTargets)
            {
                MoveAway(moveAwayTarget.GetComponent<Rigidbody>());
            }
        }

        // Calculate and apply rotation
        Quaternion targetRotation = Quaternion.LookRotation(targetVelocity);
        rBody.rotation = Quaternion.Slerp(rBody.rotation, targetRotation, Time.deltaTime * 3);

        // Always resets x rotation so the character doesn't rotate forward with physics interactions like jump or bumbs in ground.
        Quaternion resetHorizontalRotation = rBody.rotation;
        resetHorizontalRotation.z = 0;
        resetHorizontalRotation.x = 0;
        rBody.rotation = resetHorizontalRotation;
    }

    /// <summary>
    /// Makes sure the 3 characters move away from eachother when getting close
    /// </summary>
    /// <param name="otherCharacter">Player character thats getting to close</param>
    private void MoveAway(Rigidbody otherCharacter)
    {
        Vector3 direction = (rBody.position - otherCharacter.position);
        direction.y = 0;
        if (applyVelocity)
            rBody.velocity += direction * MOVE_AWAY_INFLUENCE;
        else rBody.velocity = direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerCharacterMovement>())
        {
            if (!moveAwayTargets.Contains(other))
            {
                moveAwayTargets.Add(other);
            }
            applyMoveAwayVelocity = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerCharacterMovement>())
        {
            if (moveAwayTargets.Contains(other))
            {
                moveAwayTargets.Remove(other);
            }

            if (moveAwayTargets.Count == 0)
                applyMoveAwayVelocity = false;
        }
    }
}
