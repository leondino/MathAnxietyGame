using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    private Animator animator;
    private bool applyVelocity = true;
    private bool applyMoveAwayVelocity = false;
    private List<Collider> moveAwayTargets = new List<Collider>();
    private NavMeshAgent navMeshAgent;
    private int navMeshUpdateTimer;
    public int frameTimeNavMeshUpdate = 5;

    void Awake()
    {
        rBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        followPoint = GetComponentInParent<PlayerControler>().followPoint;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshUpdateTimer = frameTimeNavMeshUpdate;
    }

    void Update()
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
        {
            applyVelocity = true;
            navMeshAgent.isStopped = false;
            animator.SetFloat("WalkSpeed", 2.2f);
        }
        else
        {
            applyVelocity = false;
            navMeshAgent.isStopped = true;
            animator.SetFloat("WalkSpeed", 1f);
        }

        // Apply movement
        if (applyVelocity)
        {
            //targetVelocity *= followSpeed;
            //targetVelocity.y = rBody.velocity.y;
            //rBody.velocity = targetVelocity;
            navMeshUpdateTimer -= 1;

            if (navMeshUpdateTimer <= 0)
            {
                navMeshAgent.destination = targetPosition;
                navMeshUpdateTimer = frameTimeNavMeshUpdate;
            }
        }

        // Apply move away from other characters movement
       if (applyMoveAwayVelocity)
       {
           foreach (Collider moveAwayTarget in moveAwayTargets)
           {
               MoveAway(moveAwayTarget.GetComponent<Rigidbody>());
           }
       }

        //Apply animation
        animator.SetFloat("Vertical", navMeshAgent.velocity.magnitude / 2.8f, 0.1f, Time.deltaTime);

        // Calculate and apply rotation
        Quaternion targetRotation = Quaternion.LookRotation(targetVelocity);
        rBody.rotation = Quaternion.Slerp(rBody.rotation, targetRotation, Time.deltaTime * 3);

        // Always resets x rotation so the character doesn't rotate forward with physics interactions like jump or bumbs in ground.
        Quaternion resetHorizontalRotation = rBody.rotation;
        resetHorizontalRotation.z = 0;
        resetHorizontalRotation.x = 0;
        rBody.rotation = resetHorizontalRotation;

        // Players stop walking/readjusting when interacting
        if(GameManager.Instance.UIIsActive || DialogueManager.Instance.dialogueBox.activeSelf)
        {
            rBody.velocity = Vector3.zero;
            navMeshAgent.ResetPath();
            animator.SetFloat("Vertical", 0, 0, Time.deltaTime);
        }
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

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.GetComponent<PlayerCharacterMovement>())
    //    {
    //        if (!moveAwayTargets.Contains(other))
    //        {
    //            moveAwayTargets.Add(other);
    //        }
    //        applyMoveAwayVelocity = true;
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.GetComponent<PlayerCharacterMovement>())
    //    {
    //        if (moveAwayTargets.Contains(other))
    //        {
    //            moveAwayTargets.Remove(other);
    //        }
    //
    //        if (moveAwayTargets.Count == 0)
    //            applyMoveAwayVelocity = false;
    //    }
    //}
}
