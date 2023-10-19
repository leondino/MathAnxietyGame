using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StrengthTooltip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(EventTrigger eventTrigger)
    {
        Debug.Log("open pop-up");
    }

    public void OnPointerLeave(EventTrigger eventTrigger)
    {
        Debug.Log("close pop-up");
    }
}
