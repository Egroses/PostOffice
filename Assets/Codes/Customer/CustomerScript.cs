using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerScript : MonoBehaviour
{
    [SerializeField] GameObject ExitPoint;

    NavMeshAgent navMesh;
    int _id;
    bool inTheOffice;
    
    void Start()
    {
        GameEvents.current.customerDuty += customerNavigation;

        navMesh = GetComponent<NavMeshAgent>();
    }
    private void OnEnable()
    {
        inTheOffice = false;
    }

    public void customerNavigation(Vector3 locationOfTarget, int id)
    {
        if (_id == id)
        {
            inTheOffice = true;
            navMesh.SetDestination(locationOfTarget);
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
    public bool getSituation()
    {
        return inTheOffice;
    }

    /*
    public GameObject takedCargoFromDesk()
    {
        int index = cargoBoxList.Count - 1;
        GameObject go = cargoBoxList[index];
        go.transform.parent = null;
        go.transform.DOMove(Customers[0].transform.position, 0.1f);
        cargoBoxList.RemoveAt(index);
        resetTheCargoPositions();
        charBalance.earnMoney(40);
        return go;
    }*/
}
