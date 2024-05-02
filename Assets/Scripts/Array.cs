using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Array : MonoBehaviour
{
    [SerializeField] int[] _NumerArray = new int[4];

    [SerializeField] string[] _Names = { "Paco", "Master Yogurt", "Chancellor Paperplane" };

    [SerializeField] GameObject[] _Objects;
    // Start is called before the first frame update
    void Start()
    {
        _NumerArray[0] = 10;

        _Objects = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _Objects = GameObject.FindGameObjectsWithTag("Enemy");
        }
    }
}
