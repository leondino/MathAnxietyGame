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
        CanGetPower = false;
        powerGlow.SetActive(true);
        HasPower = true;
    }

    public void PunchWall(GameObject wall, GameObject adjacentWall)
    {
        // Only punch when not in active UI like dialogue
        if (!GameManager.Instance.UIIsActive)
        {
            // Use UIISActive to prefent player from walking while punching
            GameManager.Instance.UIIsActive = true;
            HasPower = false;
            StartCoroutine(OnPunchWall(wall, adjacentWall));
        }
    }

    /// <summary>
    /// Player character punches wall and then destroys walls after animation. Loses power afterwards.
    /// </summary>
    /// <param name="wall"></param>
    /// <param name="adjacentWall"></param>
    /// <returns></returns>
    private IEnumerator OnPunchWall(GameObject wall, GameObject adjacentWall)
    {
        //Punch animation to be implemented here
        GetComponent<Animator>().SetBool("IsPunching", true);
        yield return new WaitForSeconds(1.15f);
        GetComponent<Animator>().SetBool("IsPunching", false);
        wall.SetActive(false);
        adjacentWall.SetActive(false);
        powerGlow.SetActive(false);
        GameManager.Instance.UIIsActive = false; ;
    }
}