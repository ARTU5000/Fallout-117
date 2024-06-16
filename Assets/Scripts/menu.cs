using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    public GameObject A;
    public GameObject B;
    // Start is called before the first frame update
    void Start()
    {
        A.SetActive(false);
        B.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Creditos()
    {
        A.SetActive(!A.activeSelf);
        if (B.activeSelf)
        {
            B.SetActive(false);
        }
    }

    public void controles()
    {
        B.SetActive(!B.activeSelf);
        if (A.activeSelf)
        {
            A.SetActive(false);
        }
    }

    public void salir()
    {
        Application.Quit();
    } 
}
