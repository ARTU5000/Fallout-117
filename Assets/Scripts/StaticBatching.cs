using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBatching : MonoBehaviour
{
    void Start()
    {
        GameObject[] staticObjects = GameObject.FindGameObjectsWithTag("StaticObject");
        foreach (GameObject obj in staticObjects)
        {
            obj.isStatic = true;
        }
        StaticBatchingUtility.Combine(staticObjects, this.gameObject);
    }
}
