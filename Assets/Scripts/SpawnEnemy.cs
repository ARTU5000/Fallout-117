using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject player; // El GameObject del jugador
    public GameObject enemy; // El GameObject del enemigo
    public float activationDistance; // La distancia a la que se activará el enemigo
    public float distanceToPlayer;
    bool active;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("No se pudo encontrar el GameObject del jugador. Asegúrate de etiquetar al jugador correctamente.");
        }

        active = false;
        activationDistance = 20f;
    }

    void Update()
    {
        if (player != null && enemy != null)
        {
            distanceToPlayer = Vector3.Distance(enemy.transform.position, player.transform.position);

            if (distanceToPlayer < activationDistance && active == false)
            {
                enemy.SetActive(true);
                active = true;
            }
            else if (distanceToPlayer > activationDistance && active == true)
            {
                enemy.SetActive(false);
                enemy.transform.localPosition = Vector3.zero;
                active = false;
            }
        }
    }
}
