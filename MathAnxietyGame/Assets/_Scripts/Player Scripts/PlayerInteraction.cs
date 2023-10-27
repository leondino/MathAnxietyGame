using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Interactable interactableObject;
    public List<Interactable> objectsInRange = new List<Interactable>();

    /// <summary>
    /// Calls the Interaction within the interactable object
    /// </summary>
    public void InteractWithInteractable()
    {
        if (objectsInRange.Count > 0)
        {
            interactableObject = CheckClosestToMe();
            if (interactableObject.CanInteract)
                interactableObject.Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Interactable>())
            objectsInRange.Add(other.GetComponent<Interactable>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Interactable>() &&
            objectsInRange.Contains(other.GetComponent<Interactable>()))
            objectsInRange.Remove(other.GetComponent<Interactable>());
    }

    /// <summary>
    /// Checks which interactable object from the interatable objects in range is closest to the player
    /// </summary>
    /// <returns>Closest interactable object to player</returns>
    private Interactable CheckClosestToMe()
    {
        Interactable closestInteractable = null;
        float smallestDistance = 100;

        foreach (Interactable interactable in objectsInRange)
        {
            float distance = Vector3.Distance(interactable.transform.position, transform.position);
            if (distance < smallestDistance)
            {
                smallestDistance = distance;
                closestInteractable = interactable;
            }
        }

        return closestInteractable;
    }
}
