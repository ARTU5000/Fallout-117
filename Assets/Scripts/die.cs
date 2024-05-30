using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class die : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();
    public Animator clone;
    public float RandTime;
    public float RandPlayer;
    // Start is called before the first frame update
    void Start()
    {
        FindPlayers();
        InvokeRepeating("Activate",3f,3f);
        RandTime = Random.Range(30, 50);
    }

    // Update is called once per frame
    void Update()
    {
        FindPlayers();
        RandTime -= Time.deltaTime;
        if (RandTime <= 0)
        {
            RandPlayer = Random.Range(0, players.Count);
            clone = GetComponent<Animator>();
            clone.SetBool("morido", true);
        }
    }

    void Activate()
    {
        foreach (GameObject a in players)
        {
            if (!a.activeSelf)
            {
                a.SetActive(true);
                break;
            }
        }
    }

    void FindPlayers()
    {
        GameObject[] targetArray = GameObject.FindGameObjectsWithTag("Player");
        players = new List<GameObject>(targetArray);
    }
}
