using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    
    public GameObject cameraFirstPerson;
   
    public void changeCamera()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (cameraFirstPerson.activeSelf)
            {
                cameraFirstPerson.SetActive(false);
            }
            else
            {
                 cameraFirstPerson.SetActive(true);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        changeCamera();
    }
}
