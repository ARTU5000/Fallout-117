using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    public bool ground;
    public bool move;
    private float jp ;
    public float movementSpeed;
    public Vector2 sensitivity;public float hor;
    Vector3 velocity;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked;  
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    
        ground = true;
        jp = 8f;
    }

    private void UpdateMovement()
    {
         hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        if (hor != 0 || ver != 0)
        {
            anim.SetBool("moving", true);
            rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
            rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
            Vector3 direction = (transform.forward * ver + transform.right * hor).normalized;

            velocity = direction * movementSpeed;
        }
        else
        {
            anim.SetBool("moving", false);
            velocity = Vector3.zero; 
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }

        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
    }
    
    void jump()
    {
        if (ground == true && Input.GetButton("Jump"))
        {
            rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
            rb.constraints &= ~RigidbodyConstraints.FreezePositionZ; 
            rb.velocity = new Vector3(rb.velocity.x, jp,rb.velocity.z);
            ground = false;
        }
    }

    private void UpdateMouseLook()
    {
        float hor = Input.GetAxis("Mouse X");
        float ver = Input.GetAxis("Mouse Y");

        if (hor != 0)
        {
            transform.Rotate(0, hor * sensitivity.x, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        UpdateMouseLook();
        //jump();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Piso")
        {
            ground = true;
        }

        if (other.gameObject.tag != "Piso"|| ground==true)
        {
            move = true;
        }
        else if (other.gameObject.tag != "Piso"|| ground==false)
        {
            move = false;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Piso")
        {
            ground = false;
        }

        if (other.gameObject.tag != "Piso")
        {
            move = true;
        }
    }
}
