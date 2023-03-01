using System.Collections;
using System.Collections.Generic;
using Codes.Char;
using UnityEngine;
using DG.Tweening;
public class CarForCargo : MonoBehaviour
{
    [SerializeField] CharObjectsList Character;
    [SerializeField] CharBalance charBalance;
    [SerializeField] GameObject putTheCargo;
    [SerializeField] GameObject StartOfRoad;
    [SerializeField] GameObject EndOfRoad;
    [SerializeField] List<GameObject> cargoBoxList = new List<GameObject>();

    bool StickmanTrigger = false;

    Vector3 VanPosition;
    Collider colliderOfVan;

    private void Start()
    {
        colliderOfVan = GetComponent<Collider>();
        VanPosition = transform.position;
    }

    IEnumerator putBoxOnCar()//scaleri ile oyna
    {
        while (true)
        {
            if (StickmanTrigger && Character.GetCountOfList() > 0 && cargoBoxList.Count < 6)
            {
                GameObject go = Character.RemoveObjectFromList();
                go.transform.parent = transform;
                
                if (cargoBoxList.Count % 2 == 0)
                {
                    go.transform.DOMove(putTheCargo.transform.position + Vector3.up * (int)(cargoBoxList.Count / 2) * 2, 0.3f);
                }
                else
                {
                    go.transform.DOMove(putTheCargo.transform.position + 2*Vector3.left + Vector3.up * (int)(cargoBoxList.Count / 2) * 2, 0.3f);
                }
                
                yield return new WaitForSeconds(0.3f);
                go.transform.DOComplete();
                cargoBoxList.Add(go);
                resetTheCargoPositions();
            }
            else
            {
                break;
            }
        }
        if (cargoBoxList.Count == 6)
        {
            colliderOfVan.enabled = false;
            carOnTheDelivery();
            yield return new WaitForSeconds(5f);
            carFinishDelivery();
            yield return new WaitForSeconds(2f);
            colliderOfVan.enabled = true;
        }
    }

    void carOnTheDelivery()
    {
        transform.DOMove(EndOfRoad.transform.position,2f);
        charBalance.EarnMoney(300);
    }

    void carFinishDelivery()
    {
        foreach (var item in cargoBoxList)
        {
            item.SetActive(false);
            item.transform.parent = null;
        }
        cargoBoxList.Clear();
        transform.position = StartOfRoad.transform.position;
        transform.DOMove(VanPosition, 2f);
    }

    void resetTheCargoPositions()
    {
        for (int i = 0; i < cargoBoxList.Count; i++)
        {
           // cargoBoxList[i].transform.position = transform.position + Vector3.up * (i + 1) * 2;
            cargoBoxList[i].transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StickmanTrigger = true;
            StartCoroutine(putBoxOnCar());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StickmanTrigger = false;
        }
    }
}
