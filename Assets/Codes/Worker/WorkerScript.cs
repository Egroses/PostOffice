using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerScript : MonoBehaviour
{
    NavMeshAgent navAgent;
    int _id;

    private void Start()
    {
        GameEvents.current.workerDuty += workerNavigation;
        navAgent = GetComponent<NavMeshAgent>();
    }

    public void workerNavigation(Vector3 locationOfTarget, int id)
    {
        if (_id == id)
        {
            navAgent.SetDestination(locationOfTarget);
        }
    }

    public void setId(int id)
    {
        _id = id;
    }

    public int getId()
    {
        return _id;
    }

}
