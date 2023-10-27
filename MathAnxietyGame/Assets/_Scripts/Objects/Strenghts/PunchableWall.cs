using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PunchableWall : Interactable
{
    private UnityAction punchAction;
    protected override void Start()
    {
        base.Start();
        punchAction += delegate { TriggerPunchWall(); };
        onInteract.AddListener(punchAction);
    }

    public void TriggerPunchWall()
    {
        foreach (GameObject player in GameManager.Instance.thePlayer.GetComponent<PlayerControler>().playerCharacters)
        {
            DestroyWall playerPunch = player.GetComponent<DestroyWall>();
            if (playerPunch.HasPower)
            {
                playerPunch.PunchWall(gameObject);
                return;
            }
        }
        Debug.Log("No power to punch a wall");
    }
}
