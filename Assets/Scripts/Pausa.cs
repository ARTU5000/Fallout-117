using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pausa : MonoBehaviour
{
    public GameObject pauseMenu;
    public Button resumeButton;
    public Button Bcontroles;
    public Button BA;
    public Button BB;
    public GameObject Controles;
    public GameObject A;
    public GameObject B;
    public cameraController CC;
    public PlayerController PC;

    private bool isPaused = false;

    void Start()
    {
        pauseMenu.SetActive(false);

        resumeButton.onClick.AddListener(TogglePause);
        Bcontroles.onClick.AddListener(ToggleObjectToActivate);
        BA.onClick.AddListener(ToggleObjectToDeactivate);
        BB.onClick.AddListener(ToggleObjectToDeactivate);


        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;

        if (isPaused)
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

    public void ToggleObjectToActivate()
    {
        Controles.SetActive(!Controles.activeSelf);
    }

    public void ToggleObjectToDeactivate()
    {
        A.SetActive(!A.activeSelf);
        B.SetActive(!A.activeSelf);
    }
}
