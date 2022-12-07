using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent onInteract;

    //! Boolean to disable Interaction
    public bool InteractionEnabled = true;

    //! Boolean property so other classes can check if this object has interaction
    public bool HasInteraction { get; set; }

    //! Property checks if the object can be interacted with (based on interaction status).
    public bool CanInteract
    {
        get { return InteractionEnabled &! HasInteraction; }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        
    }

    public virtual void Interact()
    {
        if (CanInteract)
        {
            HasInteraction = true;
            onInteract.Invoke();
        }
    }
}
