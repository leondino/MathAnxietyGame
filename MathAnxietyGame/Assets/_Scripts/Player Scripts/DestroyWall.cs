using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWall : MonoBehaviour
{
    public bool CanGetPower { get; set; } = true;
    public bool HasPower { get; set; } = false;
    [SerializeField]
    private GameObject powerGlow;

    public void PowerUp()
    {
        powerGlow.SetActive(true);
        HasPower = true;
    }

    public void PunchWall(GameObject wall, GameObject adjacentWall)
    {
        HasPower = false;
        StartCoroutine(OnPunchWall(wall, adjacentWall));
    }

    private IEnumerator OnPunchWall(GameObject wall, GameObject adjacentWall)
    {
        //Punch animation to be implemented here
        yield return new WaitForSeconds(2f);
        Destroy(wall);
        Destroy(adjacentWall);
        powerGlow.SetActive(false);
    }
}
