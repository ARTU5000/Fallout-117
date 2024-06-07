using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor; 

public class AI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator clone;
    private List<Transform> targets = new List<Transform>();
    private List<Transform> rest = new List<Transform>();

    private Queue<Transform> targetQueue = new Queue<Transform>();
    private Queue<Transform> restQueue = new Queue<Transform>();
    private Transform currentTarget;
    private bool isWorking;
    private bool isMoving;
    
    public GameObject[] prefabs;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        clone = GetComponentInChildren<Animator>();
        FindTargets();

        FillQueues();
        MoveToNextTarget();
    }

    void Update()
    {
        if (isMoving && agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
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
                        break;
                    }
                }
            }

            Invoke(nameof(MoveToNextTarget), 5f);
        }
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
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    bool IsPrefab(GameObject targetObject, GameObject prefab)
    {
        var targetPrefabType = PrefabUtility.GetPrefabAssetType(targetObject);
        var prefabType = PrefabUtility.GetPrefabAssetType(prefab);

        if (targetPrefabType == PrefabAssetType.Regular && prefabType == PrefabAssetType.Regular)
        {
            var targetPrefabParent = PrefabUtility.GetCorrespondingObjectFromSource(targetObject);
            var prefabParent = PrefabUtility.GetCorrespondingObjectFromSource(prefab);

            return targetPrefabParent == prefabParent;
        }

        return false;
    }
}