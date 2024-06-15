using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimonSaysController : MonoBehaviour
{
    public Button[] buttons; // Asigna los botones desde el inspector
    public TextMeshProUGUI infoText;
    public GameObject simonSaysUI; // La UI de Simon Dice
    public GameObject simonSaysTrigger; // El trigger del minijuego

    private List<int> sequence = new List<int>();
    private int sequenceIndex = 0;
    private int roundsPlayed = 0;
    private bool playerTurn = false;
    private bool inTriggerZone = false;

    void Start()
    {
        simonSaysUI.SetActive(false);
    }

    void Update()
    {
        if (inTriggerZone && Input.GetKeyDown(KeyCode.E))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            StartCoroutine(StartGame());
        }
    }

    IEnumerator StartGame()
    {
        roundsPlayed = 0;
        sequence.Clear();
        sequenceIndex = 0;
        simonSaysUI.SetActive(true);
        infoText.text = "Simon Dice: ¡Observa!";
        yield return new WaitForSeconds(1f);

        // Inicia la secuencia del juego
        sequence.Add(UnityEngine.Random.Range(0, buttons.Length));
        yield return StartCoroutine(PlaySequence());

        infoText.text = "Tu Turno: ¡Repite la secuencia!";
        playerTurn = true;
    }

    IEnumerator PlaySequence()
    {
        for (int i = 0; i < sequence.Count; i++)
        {
            buttons[sequence[i]].GetComponent<Image>().color = Color.white; // Cambia a blanco para indicar que está activo
            yield return new WaitForSeconds(0.5f);
            buttons[sequence[i]].GetComponent<Image>().color = buttons[sequence[i]].colors.normalColor; // Vuelve a su color original
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void ButtonPressed(int index)
    {
        if (!playerTurn)
            return;

        if (index == sequence[sequenceIndex])
        {
            sequenceIndex++;
            if (sequenceIndex >= sequence.Count)
            {
                playerTurn = false;
                sequenceIndex = 0;
                roundsPlayed++;
                if (roundsPlayed >= 3)
                {
                    GameWon();
                    return;
                }
                infoText.text = "¡Correcto! Simon Dice: ¡Observa!";
                StartCoroutine(NextRound());
            }
        }
        else
        {
            infoText.text = "¡Incorrecto! Juego terminado.";
            playerTurn = false;
            simonSaysUI.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    IEnumerator NextRound()
    {
        yield return new WaitForSeconds(1f);
        sequence.Add(UnityEngine.Random.Range(0, buttons.Length));
        yield return StartCoroutine(PlaySequence());

        infoText.text = "Tu Turno: ¡Repite la secuencia!";
        playerTurn = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == simonSaysTrigger)
        {
            inTriggerZone = true;
            infoText.text = "Presiona 'E' para jugar Simon dice";
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == simonSaysTrigger)
        {
            inTriggerZone = false;
            infoText.text = "";
        }
    }

    void GameWon()
    {
        infoText.text = "¡Felicidades, has ganado!";
        simonSaysUI.SetActive(false);
        // Lógica adicional cuando ganas el minijuego
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}


