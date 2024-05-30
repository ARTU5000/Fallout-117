using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class NotCollide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        float x = Random.Range(-19, 19);
        float z = Random.Range(-19, 19);

        if (other.tag != "Player" || other.tag != "Piso")
        transform.position = new Vector3(x, transform.position.y, z);
    }
}
