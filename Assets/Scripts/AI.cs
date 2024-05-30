using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    
    public NavMeshAgent agent;
    public List<GameObject> targets = new List<GameObject>(); 
    public Animator clone;
    public GameObject closestTarget;
    public GameObject courrentTarget;
    public GameObject deathbeacon;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        clone.SetBool("alto", true);
        Invoke("Stop", 5);
        closestTarget = null;
    }

    void Update()
    {
        if (clone.GetBool("morido"))
        {
            deathbeacon.SetActive(true);
        }
        else
        {
            deathbeacon.SetActive(false);
        }

        if (clone.GetBool("morido") && !clone.GetCurrentAnimatorStateInfo(0).IsName("Dying Backwards"))
        {
            deathbeacon.SetActive(true);
            Invoke("Death", 5);
        }

        if (!clone.GetBool("alto") && closestTarget == null && !clone.GetBool("morido"))
        {
            FindTargets();
            closestTarget = GetClosestTarget();
        }

        if (closestTarget != null)
        {
            agent.SetDestination(closestTarget.transform.position);

            if (!closestTarget.activeSelf)
            {
                closestTarget = this.gameObject;
                agent.isStopped = true;
                clone.SetBool("alto", true);
                Invoke("Stop", 3);
            }
            else
            {
                agent.isStopped = false;
            }
        }
    }

    void Stop()
    {
        clone.SetBool("alto", false);
        closestTarget = null;
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

    void FindTargets()
    {
        GameObject[] targetArray = GameObject.FindGameObjectsWithTag("Target");
        targets = new List<GameObject>(targetArray); 
    }

    void Death()
    {
        this.gameObject.SetActive(false);
    }

}

