using System.Collections;
using System.Collections.Generic;
using Codes.Char;
using UnityEngine;
using DG.Tweening;
public class BellScript : MonoBehaviour
{
    [SerializeField] DeskForCargo deskForCargo;
    [SerializeField] CharBalance charBalance;
    [SerializeField] GameObject ExitPoint;
 
    bool triggered=false;
    GameObject customer;

    private void OnEnable()
    {
        GameEvents.current.CustomerWalking(transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Customer"))
        {
            triggered = true;
            customer = other.gameObject;
            StartCoroutine(customerWantCargo(customer));
        }
    }

    IEnumerator customerWantCargo(GameObject customerObject)
    {
        if (deskForCargo.getCountOfList() > 0)
        {
            triggered = false;
            GameObject cargoBox = deskForCargo.takeBoxFromDesk();
            GameObject referencePoint = customerObject.transform.GetChild(0).gameObject;
            cargoBox.transform.DOMove(transform.position, 1f);
            yield return new WaitForSeconds(1f);
            cargoBox.transform.rotation = referencePoint.transform.rotation;
            cargoBox.transform.position = referencePoint.transform.position;
            cargoBox.transform.parent = customerObject.transform;
            cargoBox.GetComponent<Collider>().enabled = true;

            int id = customerObject.GetComponent<CustomerScript>().getId();
            GameEvents.current.CustomerWalking(ExitPoint.transform.position,id);
            charBalance.EarnMoney(40);
            GameEvents.current.CustomerWalking(transform.position);
        }
    }

    public void getActiveTheGiveCargo()
    {
        if (triggered)
        {
            StartCoroutine(customerWantCargo(customer));
        }
    }
}
