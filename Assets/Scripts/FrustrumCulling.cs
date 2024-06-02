using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrustrumCulling : MonoBehaviour
{
    
    public Camera mainCamera;
    private Renderer[] renderers;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        renderers = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        if (renderers == null || mainCamera == null) return;

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        foreach (Renderer renderer in renderers)
        {
            if (GeometryUtility.TestPlanesAABB(planes, renderer.bounds))
            {
                renderer.enabled = true;
            }
            else
            {
                renderer.enabled = false;
            }
        }
    }
}
