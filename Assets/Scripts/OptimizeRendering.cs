using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptimizeRendering : MonoBehaviour
{
    public Camera mainCamera;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (mainCamera != null)
        {
            mainCamera.useOcclusionCulling = true;
        }
    }
}
