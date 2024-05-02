using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class listas : MonoBehaviour
{
    public List<string> _numeros = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        _numeros.Add("Paco");
        _numeros.Add("Cesarin");
        _numeros.Add("Mico");

        _numeros.Remove("Cesarin");
        _numeros.Insert(1, "Ivan");

        Debug.Log(_numeros[1]);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
