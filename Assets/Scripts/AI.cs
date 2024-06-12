using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor; 
using UnityEngine.UI;
using TMPro;
using System.IO;
using System;

public class AI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator clone;
    private List<Transform> targets = new List<Transform>();
    private List<Transform> rest = new List<Transform>();

    private Queue<Transform> targetQueue = new Queue<Transform>();
    private Queue<Transform> restQueue = new Queue<Transform>();
    [SerializeField]private Transform currentTarget;
    private bool isWorking;
    private bool isMoving;
    
    private float targetTimeout = 30f;
    private float targetTimer;

    public GameObject[] prefabs;
    public GameObject[] prefabsRest;
    [SerializeField]int[] resource = new int[3]; //0 = energia, 1 = agua, 2 = comida
    public TextMeshProUGUI Pointer;
    SaveResources SaveP;
    string rute;

    void Start()
    {
        Load();
        agent = GetComponent<NavMeshAgent>();
        clone = GetComponentInChildren<Animator>();
        FindTargets();

        FillQueues();
        MoveToNextTarget();
    }

    void Update()
    {
        if (isMoving)
        {
            targetTimer += Time.deltaTime;

            if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            {
                isMoving = false;
                clone.SetBool("moving", false);

                if (isWorking && currentTarget != null && currentTarget.gameObject.layer == LayerMask.NameToLayer("Target"))
                {
                    clone.SetBool("work", true);

                    for (int i = 0; i < prefabs.Length; i++)
                    {
                        if (IsPrefab(currentTarget.gameObject, prefabs[i]))
                        {
                            if (i < 3)
                            {
                                Load();
                                resource[i] ++;
                                Save();
                            }
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < prefabsRest.Length; i++)
                    {
                        if (IsPrefab(currentTarget.gameObject, prefabsRest[i]))
                        {
                            if (i < 3)
                            {
                                Load();
                                int randomIndex = UnityEngine.Random.Range(0, resource.Length);
                                resource[randomIndex]--;
                                randomIndex = UnityEngine.Random.Range(0, resource.Length);
                                resource[randomIndex]--;
                                Save();
                            }
                            break;
                        }
                    }
                }

                Invoke(nameof(MoveToNextTarget), 5f);
            }
            else if (targetTimer >= targetTimeout)
            {
                MoveToNextTarget();
            }
        }

        Load();
        Pointer.text = "Energia: " + resource[0].ToString() + "\n\n\n Agua: " + resource[1].ToString() + "\n\n\n Comida: " + resource[2].ToString();
    }

    void FindTargets()
    {
        GameObject[] targetObjects = GameObject.FindGameObjectsWithTag("Target");

        foreach (GameObject obj in targetObjects)
        {
            if (obj.layer == LayerMask.NameToLayer("Target"))
            {
                targets.Add(obj.transform);
            }
            else if (obj.layer == LayerMask.NameToLayer("Rest"))
            {
                rest.Add(obj.transform);
            }
        }
    }

    void MoveToNextTarget()
    {
        if (targetQueue.Count == 0 && restQueue.Count == 0)
        {
            FillQueues();
        }

        if (targetQueue.Count > 0)
        {
            currentTarget = targetQueue.Dequeue();
            isWorking = true;
        }
        else if (restQueue.Count > 0)
        {
            currentTarget = restQueue.Dequeue();
            isWorking = false;
        }

        if (currentTarget != null)
        {
            agent.SetDestination(currentTarget.position);
            clone.SetBool("moving", true);
            clone.SetBool("work", false);
            isMoving = true;
            targetTimer = 0f; // Reset the timer when a new target is set
        }
    }

    void FillQueues()
    {
        targetQueue.Clear();
        restQueue.Clear();

        List<Transform> shuffledTargets = new List<Transform>(targets);
        List<Transform> shuffledRests = new List<Transform>(rest);

        Shuffle(shuffledTargets);
        Shuffle(shuffledRests);

        for (int i = 0; i < 5 && i < shuffledTargets.Count; i++)
        {
            targetQueue.Enqueue(shuffledTargets[i]);
        }

        for (int i = 0; i < 2 && i < shuffledRests.Count; i++)
        {
            restQueue.Enqueue(shuffledRests[i]);
        }
    }

    void Shuffle(List<Transform> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Transform temp = list[i];
            int randomIndex = UnityEngine.Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    bool IsPrefab(GameObject targetObject, GameObject prefab)
    {
        return targetObject.name == prefab.name;
    }

    public void Save()
    {
        rute = Application.streamingAssetsPath + "/Resources.json";
        SaveP = new SaveResources(resource[0], resource[1], resource[2]);
        string json = JsonUtility.ToJson(SaveP, true);
        System.IO.File.WriteAllText(rute, json);
    }

    public void Load()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "Resources.json");

        if (File.Exists(filePath))
        {
            rute = Application.streamingAssetsPath + "/Resources.json";
            string json = System.IO.File.ReadAllText(rute);
            SaveP = JsonUtility.FromJson<SaveResources>(json);

            resource[0] = (SaveP.NRG);
            resource[1] = (SaveP.WTR);
            resource[2] = (SaveP.FUD);
        }
        else
        {
            resource[0] = 0;
            resource[1] = 0;
            resource[2] = 0;
            Save();
        }
    }
}