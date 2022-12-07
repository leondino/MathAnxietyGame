using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple script to sent collision of rope to parent object (no rigidbody)
/// </summary>
public class RopeCollisionDetecter : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerCharacterMovement>())
        {
            GetComponentInParent<RopePlayerInteraction>().JumpRopeFailed();
        }
    }
}
