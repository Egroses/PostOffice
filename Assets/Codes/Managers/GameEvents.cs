using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    public delegate void EventsOfMine(Vector3 location, int id);

    public EventsOfMine customerDuty;
    public EventsOfMine workerDuty;

    public List<GameObject> Desks = new List<GameObject>();
    public List<GameObject> Stacks = new List<GameObject>();

    int _idC = 0;

    private void Awake()
    {
        current = this;
    }

    public void CustomerWalking(Vector3 targetLocation)
    {
        customerDuty(targetLocation, _idC);
        _idC++;
    }
    public void CustomerWalking(Vector3 targetLocation, int __id)
    {
        customerDuty(targetLocation, __id);
    }
    
    public void WorkerTakeCargo( int __id)
    {
        customerDuty( Desks[ Random.Range( 0 , Stacks.Count ) ].transform.position, __id);
    }
    public void WorkerPutCargo( int __id)
    {
        customerDuty( Desks [ Random.Range( 0 , Desks.Count ) ].transform.position, __id);
    }

}
