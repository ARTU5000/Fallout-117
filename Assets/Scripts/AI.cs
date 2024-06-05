using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    
    public NavMeshAgent agent;
    public List<GameObject> targetsR = new List<GameObject>(); 
    public List<GameObject> targetsW = new List<GameObject>(); 
    public List<GameObject> targets = new List<GameObject>(); 
    public Animator clone;
    public GameObject closestTarget;
    public GameObject courrentTarget;
    public bool isWorking;
   [SerializeField] private int count;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        clone = GetComponentInChildren<Animator>();
        clone.SetBool("moving", false);
        Invoke("Stop", 5);
        closestTarget = null;
        isWorking = true;
        count = 0;
    }

    void Update()
    {
        if (!clone.GetBool("moving") && closestTarget == null)
        {
            FindTargets();
            closestTarget = GetRandomTarget();
            clone.SetBool("moving", true);
        }

        if (closestTarget != null)
        {
            agent.SetDestination(closestTarget.transform.position);

            if (!closestTarget.activeSelf)
            {
                closestTarget = this.gameObject;
                agent.isStopped = true;
                //clone.SetBool("moving", false);
                //Invoke("Stop", 5);
            }
            else
            {
                agent.isStopped = false;
            }
        }
    }

    void Stop()
    {
        clone.SetBool("moving", false);
        closestTarget = null;
        if (isWorking && agent.isStopped)
        {
            clone.SetBool("work", true);
            Invoke("StopW", 5);
        }
    }
    void StopW()
    {
        clone.SetBool("work", false);
    }

    GameObject GetClosestTarget()
    {
        GameObject ClosestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject target in targets)
        {
            if (target == null) continue;

            float dist = Vector3.Distance(agent.transform.position, target.transform.position);

            if (dist < closestDistance)
            {
                closestDistance = dist;
                ClosestTarget = target;
            }
        }

        return ClosestTarget;
    }

    GameObject GetRandomTarget()
    {
        List<GameObject> Targets = new List<GameObject>();
        foreach (GameObject target in targets)
        {
            if (target != null)
            {
                Targets.Add(target);
            }
        }

        if (Targets.Count == 0)
        {
            return null;
        }

        int rnd = Random.Range(0, Targets.Count);
        return Targets[rnd];
    }

    void FindTargets()
    {
        GameObject[] targetRest = GameObject.FindGameObjectsWithTag("Rest");
        GameObject[] targetWorks = GameObject.FindGameObjectsWithTag("Target");
        targetsR = new List<GameObject>(targetRest); 
        targetsW = new List<GameObject>(targetWorks);
        if (isWorking && count < 5)
        {
            targets = targetsW;
            count++;
        }
        else if(count>= 5 && isWorking)
        {
            count = 1;
            isWorking = false;
            targets = targetsR;
        }
        else if(!isWorking && count < 2)
        {
            targets = targetsR;
            count++;
        }
        else if (count >= 2 && !isWorking)
        {
            count = 1;
            isWorking = true;
            targets = targetsW;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == closestTarget)
        {
            clone.SetBool("moving", false);
            Stop();
        }
    }
}

