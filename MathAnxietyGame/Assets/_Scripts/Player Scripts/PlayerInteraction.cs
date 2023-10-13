using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private Interactable interactableObject;

    /// <summary>
    /// Calls the Interaction within the interactable object
    /// </summary>
    public void InteractWithInteractable()
    {
        if (interactableObject != null)
            if (interactableObject.CanInteract)
                interactableObject.Interact();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Interactable>())
            interactableObject = other.GetComponent<Interactable>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Interactable>() && 
            interactableObject == other.GetComponent<Interactable>())
            interactableObject = null;
    }
}
