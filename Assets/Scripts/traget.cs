using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using TMPro;

public class traget : MonoBehaviour
{public Transform npc;
    public GameObject targetPrefabs;
    public GameObject playerPrefabs;
    private List<GameObject> targets = new List<GameObject>();
    private List<GameObject> player = new List<GameObject>();
    private List<GameObject> playerA = new List<GameObject>();
    private float spawn = .2f;
    public int totalTargets;
    public TextMeshProUGUI targetNumber;
    public int totalplayers;
    public TextMeshProUGUI playersNumber;
    public GameObject[] spawnPoints;
    public Animator clone;
    public float RandTime;
    public int RandPlayer;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ActivateTarget", 5f, 10f);
        RandTime = Random.Range(10, 50);
        GameObject[] targetArray = GameObject.FindGameObjectsWithTag("Target");
        targets = new List<GameObject>(targetArray); 
    }

    // Update is called once per frame
    void Update()
    {
        totalTargets = Activetargets();
        targetNumber.text = "Targets Activos: " + totalTargets;
        totalplayers = Activeplayers();
        playersNumber.text = "Clones Activos: " + totalplayers;

        if (targets.Count >= 300) 
        {
            CancelInvoke("SpawnTarget");
        }
        if (player.Count >= 300)
        {
            CancelInvoke("players");
        }

        playerA.Clear();
        playerA = player.FindAll(go => go.activeInHierarchy);
        RandPlayer = Random.Range(0, playerA.Count);
    }

    int Activetargets()
    {
        int TotalTargets = 0;
        foreach (GameObject target in targets)
        {
            if (target.activeSelf)
            {
                TotalTargets++;
            }
        }
        return TotalTargets;
    }
    int Activeplayers()
    {
        int TotalTargets = 0;
        foreach (GameObject target in player)
        {
            if (target.activeSelf)
            {
                TotalTargets++;
            }
        }
        return TotalTargets;
    }


    private void ActivateTarget()
    {
        foreach (GameObject target in targets)
        {
            if (target == null) continue;
            if (!target.activeSelf)
            {
                target.SetActive(true);
                break;
            }
        }
    }
}
