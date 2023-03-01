using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CustmerCreate : MonoBehaviour
{
    [SerializeField] List<GameObject> Customers = new List<GameObject>();
    [SerializeField] string nameOfCustomer;
    [SerializeField] float waitLine;
    [SerializeField] int maxCustomerAmount;

    int id = 0;

    void Start()
    {
        StartCoroutine(CustomerSpawn());
    }

    private IEnumerator CustomerSpawn()
    {
        while (Customers.Count < maxCustomerAmount)
        {
            GameObject customer = ObjectPool.Instance.GetFromPool(nameOfCustomer);
            Customers.Add(customer);
            customer.SetActive(true);
            customer.transform.position = transform.position;
            customer.transform.DOMove(transform.position + transform.forward * ((maxCustomerAmount - Customers.Count) * waitLine), 0.2f);//animasyon eklerim
            customer.GetComponent<CustomerScript>().setId(id);
            id++;
            yield return new WaitForSeconds(1f);
        }
    }

    public void CustomerSpawning()
    {
        Customers.RemoveAt(0);
        float posAmount = 0;
        foreach (GameObject obj in Customers)
        {
            if (!obj.GetComponent<CustomerScript>().getSituation())
            {
                obj.transform.DOMove(transform.position + transform.forward * (maxCustomerAmount - posAmount) * waitLine, 0.2f);
                posAmount++;
            }
        }
        StartCoroutine(CustomerSpawn());
    }
}
