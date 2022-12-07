using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script makes the jump rope rotate (increasingly fast) around so the players can jump over it
/// </summary>
public class JumpRopeRotation : MonoBehaviour
{
    [SerializeField]
    private Transform rotationPoint;
    [SerializeField]
    private float startRotateSpeed;
    private float rotateSpeed;
    [SerializeField]
    private float speedIncrease;
    [SerializeField]
    public int amountOfSwings; 
    private int previousAmountOfSwings;

    private float currentDegrees = 0;

    void Awake()
    {
        //rotateSpeed = startRotateSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RotateRope();
        CalculateSwings();
    }

    /// <summary>
    /// Rotate the rope around on the X-axis based on degrees
    /// </summary>
    private void RotateRope()
    {
        currentDegrees -= rotateSpeed;
        Quaternion newRotation = Quaternion.Euler(currentDegrees, 0, 0);
        rotationPoint.rotation = newRotation;
    }

    /// <summary>
    /// Starts the rotation by chaning the rotatespeed
    /// </summary>
    public void StartRotating()
    {
        rotateSpeed = startRotateSpeed;
    }

    /// <summary>
    /// Resets the jump rope to the original state
    /// </summary>
    public void ResetJumpRotation()
    {
        currentDegrees = 0;
        previousAmountOfSwings = 0;
        amountOfSwings = 0;
        rotateSpeed = startRotateSpeed;
        rotationPoint.rotation = Quaternion.Euler(0, 0, 0);
        Debug.Log("testestets");
    }

    /// <summary>
    /// Stops the jump rope from rotating at all
    /// </summary>
    public void StopRotation()
    {
        ResetJumpRotation();
        rotateSpeed = 0;
    }

    /// <summary>
    /// Keeps track of the amount of swings and increases speed each swing
    /// </summary>
    private void CalculateSwings()
    {
        amountOfSwings = (int)(currentDegrees / -360);
        if (previousAmountOfSwings < amountOfSwings)
        {
            previousAmountOfSwings = amountOfSwings;
            rotateSpeed += speedIncrease;
            Debug.Log(rotateSpeed);
        }
    }
}
