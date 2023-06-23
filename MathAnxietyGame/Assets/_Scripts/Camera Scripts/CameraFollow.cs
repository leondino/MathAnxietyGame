using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! Makes the camera follow the player
/// <summary>
/// Makes the camera look at the player.
/// Sets the camera's transform to the players' transform with an offset.
/// Interpolates between the current camera position and the target position with a certain speed.
/// </summary>
public class CameraFollow : MonoBehaviour
{
    public PlayerControler player;
    public Vector3 offset;
    public float speed;

    void FixedUpdate()
    {
        Vector3 target = Vector3.Lerp(this.transform.position, player.playersTrueCenter - offset, speed * Time.deltaTime);
        this.transform.position = target;
        transform.LookAt(player.centerPoint.transform.position);
    }
}
