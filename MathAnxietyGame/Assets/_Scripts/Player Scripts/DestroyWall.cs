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

    public void PunchWall()
    {
        HasPower = false;
        StartCoroutine(OnPunchWall());
    }

    private IEnumerator OnPunchWall()
    {
        //Punch animation to be implemented here
        yield return new WaitForSeconds(2f);
        powerGlow.SetActive(false);
    }
}
