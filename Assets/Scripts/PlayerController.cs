using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

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
    public GameObject[] players;
    public int acPlayer;
    SavePlayer SaveP;
    string rute;
    public GameObject CPlayer;
    public TextMeshProUGUI infoText;
    public GameObject elTrigger;
    private bool inTriggerZone = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        foreach (var p in players)
        {
            p.SetActive(false);
        }
        ChangePlayer();
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
        //jump();

        if (inTriggerZone && Input.GetKeyDown(KeyCode.E))
        {
            CPlayer.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (!CPlayer.activeSelf)
        {
            UpdateMovement();
            UpdateMouseLook();
        }
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == elTrigger)
        {
            inTriggerZone = true;
            infoText.text = "Presiona 'E' para Cambiar tu apariencia";
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == elTrigger)
        {
            inTriggerZone = false;
            infoText.text = "";
        }
    }

    public void Save()
    {
        rute = Application.streamingAssetsPath + "/SavePlayer.json";
        SaveP = new SavePlayer(acPlayer);
        string json = JsonUtility.ToJson(SaveP, true);
        System.IO.File.WriteAllText(rute, json);
    }

    public void Load()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "SavePlayer.json");

        if (File.Exists(filePath))
        {
            rute = Application.streamingAssetsPath + "/SavePlayer.json";
            string json = System.IO.File.ReadAllText(rute);
            SaveP = JsonUtility.FromJson<SavePlayer>(json);

            acPlayer = (SaveP._2B);
        }
        else
        {
            acPlayer = 0;
            Save();
        }
    }

    void ChangePlayer()
    {
        foreach (var p in players)
        {
            p.SetActive(false);
        }
        Load();
        players[acPlayer].SetActive(true);
        anim = players[acPlayer].GetComponent<Animator>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void P_2B()
    {
        acPlayer = 0;
        Save();
        ChangePlayer();
        CPlayer.SetActive(false);
    }
    public void P_Curie()
    {
        acPlayer = 1;
        Save();
        ChangePlayer();
        CPlayer.SetActive(false);
    }
    public void P_Mary()
    {
        acPlayer = 2;
        Save();
        ChangePlayer();
        CPlayer.SetActive(false);
    }
    public void P_Judy()
    {
        acPlayer = 3;
        Save();
        ChangePlayer();
        CPlayer.SetActive(false);
    }
    public void P_Nora()
    {
        acPlayer = 4;
        Save();
        ChangePlayer();
        CPlayer.SetActive(false);
    }
    public void P_Yennfer()
    {
        acPlayer = 5;
        Save();
        ChangePlayer();
        CPlayer.SetActive(false);
    }
    public void P_Bob()
    {
        acPlayer = 6;
        Save();
        ChangePlayer();
        CPlayer.SetActive(false);
    }
    public void P_Danse()
    {
        acPlayer = 7;
        Save();
        ChangePlayer();
        CPlayer.SetActive(false);
    }
    public void P_Dante()
    {
        acPlayer = 8;
        Save();
        ChangePlayer();
        CPlayer.SetActive(false);
    }
    public void P_Henry()
    {
        acPlayer = 9;
        Save();
        ChangePlayer();
        CPlayer.SetActive(false);
    }
    public void P_Leon()
    {
        acPlayer = 10;
        Save();
        ChangePlayer();
        CPlayer.SetActive(false);
    }
    public void P_Todd()
    {
        acPlayer = 11;
        Save();
        ChangePlayer();
        CPlayer.SetActive(false);
    }
}

[System.Serializable]
public class SavePlayer
{
    public int _2B; //Energy

    public SavePlayer(int _2B)
    {
        this._2B = _2B;
    }
}