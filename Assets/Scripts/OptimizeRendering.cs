using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptimizeRendering : MonoBehaviour
{
    public Camera[] cameras;

    void Start()
    {
        cameras = FindObjectsOfType<Camera>();
        
        if (cameras == null || cameras.Length == 0)
        {
            Debug.LogError("No cameras assigned!");
            return;
        }

        foreach (var cam in cameras)
        {
            if (cam == null)
            {
                Debug.LogWarning("A camera in the array is null. Skipping...");
                continue;
            }

            cam.useOcclusionCulling = true;
        }
    }
}
