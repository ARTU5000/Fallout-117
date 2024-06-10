using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIP_BOY : MonoBehaviour
{
    public GameObject pipboy;
    public GameObject items;
    public GameObject map;
    public GameObject online;

    bool onOff;
    // Start is called before the first frame update
    void Start()
    {
        onOff = false;
        pipboy.SetActive(onOff);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) 
        {
            onOff = !onOff;
        }
        pipboy.SetActive(onOff);
    }

    public void Items()
    {
        items.SetActive(true);
        map.SetActive(false);
        online.SetActive(false);
    }
    public void Map()
    {
        items.SetActive(false);
        map.SetActive(true);
        online.SetActive(false);
    }
    public void Online()
    {
        items.SetActive(false);
        map.SetActive(false);
        online.SetActive(true);
    }
    public void Radio()
    {

    }
}
