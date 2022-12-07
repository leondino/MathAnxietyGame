using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody rBody;
    [SerializeField]
    private float jumpPower;

    /// <summary>
    /// Checks if the player is on the ground with a raycast
    /// </summary>
    private bool IsGrounded
    {
        get
        {
            return Physics.Raycast(
                transform.position - Vector3.up * (transform.localScale.y - 0.01f), 
                -Vector3.up, 0.1f);
        }
    }

    void Awake()
    {
        rBody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Gives the playerCharacter a jump force when grounded
    /// </summary>
    public void Jump()
    {
        if (IsGrounded)
            rBody.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position - Vector3.up * transform.localScale.y, -Vector3.up * 0.1f);
    }
}
