using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lowers the door wall of the emotion discussion chamber
/// </summary>
public class LowerDoor : MonoBehaviour
{
    [SerializeField]
    private float loweringSpeed = 0.05f;
    
    /// <summary>
    /// Brings down one of the walls to make it a doorway
    /// </summary>
    public void LowerRoomDoor()
    {
        StartCoroutine(loweringDoorPosition());
    }

    private IEnumerator loweringDoorPosition()
    {
        while (transform.position.y > -3)
        {
            Vector3 position = transform.position;
            position.y -= loweringSpeed;
            transform.position = position;
            yield return new WaitForFixedUpdate();
        }
    }
}
