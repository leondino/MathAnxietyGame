using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bilboard : MonoBehaviour
{
    public Transform mainCamera;

    //! Sets the UI canvas towards the camera
    public void SetBilboard()
    {
        transform.LookAt(transform.position + mainCamera.forward);
    }
}
