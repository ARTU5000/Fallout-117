using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptimizeLightning : MonoBehaviour
{
    void Start()
    {
        QualitySettings.shadowCascades = 2; 
        QualitySettings.shadowDistance = 50;

        Light[] lights = FindObjectsOfType<Light>();
        foreach (Light light in lights)
        {
            if (light.type == LightType.Point || light.type == LightType.Spot)
            {
                light.shadows = LightShadows.Hard; 
                light.shadowResolution = UnityEngine.Rendering.LightShadowResolution.Medium; // Reduce la resolución de las sombras
            }
        }
    }
}
