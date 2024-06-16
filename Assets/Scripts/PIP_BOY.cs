using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIP_BOY : MonoBehaviour
{
    public GameObject pipboy;
    public GameObject items;
    public GameObject map;
    public GameObject online;
    public GameObject vaultboy;
    private PlayerController PC;
    private cameraController CC;

    bool onOff;
    // Start is called before the first frame update
    void Start()
    {
        PC = GetComponent<PlayerController>();
        CC = GetComponent<cameraController>();
        onOff = false;
        pipboy.SetActive(onOff);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            vaultboy.SetActive(true);
            items.SetActive(false);
            map.SetActive(false);
            online.SetActive(false);
            onOff = !onOff;

            if (onOff)
            {
                PC.enabled = false;
                CC.enabled = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                PC.enabled = true;
                CC.enabled = true;
            }
        }
        pipboy.SetActive(onOff);
    }

    public void Items()
    {
        vaultboy.SetActive(false);
        items.SetActive(true);
        map.SetActive(false);
        online.SetActive(false);
    }
    public void Map()
    {
        vaultboy.SetActive(false);
        items.SetActive(false);
        map.SetActive(true);
        online.SetActive(false);
    }
    public void Online()
    {
        vaultboy.SetActive(false);
        items.SetActive(false);
        map.SetActive(false);
        online.SetActive(true);
    }
    public void Radio()
    {

    }
}
