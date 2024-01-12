using System.Collections.Generic;
using UnityEngine;

public class WaterCulling : MonoBehaviour
{
    //! Layermask of water layer
    private int waterLayerMask = 1 << 4;
    private RaycastHit hit;

    private Camera camera;

    private Ray upperLeftRay, upperRightRay, lowerLeftRay, lowerRightRay;

    //! Keeps track of which water planes should be updated each frame
    private List<GameObject> currentWaterPlanes = new List<GameObject>();

    private void Awake()
    {
        camera = Camera.main;
    }

    private void FixedUpdate()
    {
        CalculateRays();
        CheckWaterInFrame();
    }

    /// <summary>
    /// Fires a raycast in a given direction to check if it hits water.
    /// </summary>
    /// <param name="direction">Direction of raycast fire</param>
    private void FireRaycast(Ray ray)
    {
        // Does the ray intersect any water planes
        if (Physics.Raycast(ray, out hit, camera.farClipPlane, waterLayerMask))
        {
            GameObject hitWater = hit.collider.gameObject;
            if (!currentWaterPlanes.Contains(hitWater))
                currentWaterPlanes.Add(hitWater);
            Debug.DrawRay(ray.origin, ray.direction.normalized * camera.farClipPlane, Color.yellow);
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction.normalized * camera.farClipPlane, Color.white);
        }
    }

    /// <summary>
    /// Fires 4 raycasts to check which water is in frame and should be updated
    /// </summary>
    private void CheckWaterInFrame()
    {
        FireRaycast(upperLeftRay);
        FireRaycast(upperRightRay);
        FireRaycast(lowerLeftRay);
        FireRaycast(lowerRightRay);

        foreach (GameObject water in currentWaterPlanes)
        {
            water.GetComponent<WaterTerrain>().UpdateWater();
        }
        currentWaterPlanes.Clear();
    }

    /// <summary>
    /// Calculate 4 rays based on camera corners
    /// </summary>
    private void CalculateRays()
    {
        upperLeftRay = camera.ViewportPointToRay(new Vector3(0, 1, 0));
        upperRightRay = camera.ViewportPointToRay(new Vector3(1, 1, 0));
        lowerLeftRay = camera.ViewportPointToRay(new Vector3(0, 0, 0));
        lowerRightRay = camera.ViewportPointToRay(new Vector3(1, 0, 0));
    }
}
