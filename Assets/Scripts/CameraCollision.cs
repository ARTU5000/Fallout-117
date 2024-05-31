using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    private float minDis = .5f;
    private float MaxDis = 1;
    private float suavidad = 10;
    private float dis;

    Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        dir=transform.localPosition.normalized;
        dis=transform.localPosition.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posCam =transform.parent.TransformPoint(dir*MaxDis);

        
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(MaxDis == 2)
            {
                MaxDis = 1;
            }
            else
            {
                MaxDis = 2;
            }
        }

        RaycastHit hit;
        if(Physics.Linecast(transform.parent.position, posCam, out hit))
        {
            dis =Mathf.Clamp(hit.distance *0.85f, minDis, MaxDis);
        }
        else
        {
            dis = MaxDis;
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, dir*dis, suavidad*Time.deltaTime);
    }
}
