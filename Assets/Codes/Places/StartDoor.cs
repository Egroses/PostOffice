using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDoor : MonoBehaviour
{
    [SerializeField] CustmerCreate custmerCreate;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Customer"))
        {
            custmerCreate.CustomerSpawning();
        }
    }
}
