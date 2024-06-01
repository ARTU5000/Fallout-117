using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opendoor : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("open", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        anim.SetBool("open", true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        anim.SetBool("open", true);
    }
    
    private void OnTriggerExit(Collider other)
    {
        anim.SetBool("open", false);
    }
}
