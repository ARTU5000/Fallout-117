using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    private new Rigidbody rigidbody;

    public bool OnOff;
    public Vector2 sensitivity;
    public new Transform camera;
    public GameObject tpcamera;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        OnOff = false;
        tpcamera.SetActive(OnOff);
    }

    public void changeCamera()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            OnOff = !OnOff;
            tpcamera.SetActive(OnOff);
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

        if (ver != 0)
        {
            Vector3 rotation = camera.localEulerAngles;
            rotation.x = (rotation.x - ver * sensitivity.y + 360) % 360;
            if (rotation.x > 80 && rotation.x < 180) { rotation.x = 80; } else
            if (rotation.x < 280 && rotation.x > 180) { rotation.x = 280; }

            camera.localEulerAngles = rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseLook();
        changeCamera();
    }
}
