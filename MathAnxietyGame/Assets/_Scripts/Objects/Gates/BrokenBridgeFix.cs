using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Small script that fixes the broken bridge
/// </summary>
public class BrokenBridgeFix : MonoBehaviour
{
    [SerializeField]
    private GameObject bridge, brokenBridge, plankPile;

    /// <summary>
    /// Enables/disables bridge objects to fix the bridge
    /// </summary>
    public void RepairBridge()
    {
        plankPile.SetActive(false);
        brokenBridge.SetActive(false);
        bridge.SetActive(true);
    }
}
