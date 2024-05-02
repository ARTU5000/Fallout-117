using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private new Rigidbody rigidbody;

    public bool ground;
    public bool move;
    private float jp ;
    public float movementSpeed;
    public Vector2 sensitivity;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        ground = true;
        jp = 8f;
    }

    private void UpdateMovement()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        Vector3 velocity = Vector3.zero;

        if (hor != 0 || ver != 0)
        {
            Vector3 direction = (transform.forward * ver + transform.right * hor).normalized;

            velocity = direction * movementSpeed;
        }

        velocity.y = rigidbody.velocity.y;
        rigidbody.velocity = velocity;
    }
    
    void jump()
    {
        if (ground == true && Input.GetButton("Jump"))
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, jp,rigidbody.velocity.z);
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
        jump();
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
