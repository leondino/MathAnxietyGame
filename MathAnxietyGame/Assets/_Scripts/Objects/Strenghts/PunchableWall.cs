using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PunchableWall : Interactable
{
    private UnityAction punchAction;
    [SerializeField]
    private GameObject adjacentWall;
    protected override void Start()
    {
        base.Start();
        punchAction += delegate { TriggerPunchWall(); };
        onInteract.AddListener(punchAction);
        adjacentWall = GetAdjacentWall();
    }

    /// <summary>
    /// Checks for each player if they can punch the wall, then triggers their punchwall code
    /// </summary>
    public void TriggerPunchWall()
    {
        foreach (GameObject player in GameManager.Instance.thePlayer.GetComponent<PlayerControler>().playerCharacters)
        {
            DestroyWall playerPunch = player.GetComponent<DestroyWall>();
            if (playerPunch.HasPower)
            {
                playerPunch.PunchWall(gameObject, adjacentWall);
                return;
            }
        }
        Debug.Log("No power to punch a wall");
    }

    /// <summary>
    /// Finds adjacent wall (to remove as well when being punched)
    /// </summary>
    GameObject GetAdjacentWall()
    {
        PunchableWall[] walls = transform.parent.GetComponentsInChildren<PunchableWall>();
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (PunchableWall wall in walls)
        {
            if (wall != this)
            {
                Transform potentialTarget = wall.transform;
                Vector3 directionToTarget = potentialTarget.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget;
                }
            }
        }

        return bestTarget.gameObject;
    }
}
