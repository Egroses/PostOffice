using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DeskForCargo: MonoBehaviour
{
    [SerializeField] BellScript bellScript;
    [SerializeField] CharObjectsList Character;
    [SerializeField] List<GameObject> cargoBoxList = new List<GameObject>();
    
    bool StickmanTrigger = false;

    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator putBoxOnDesk()
    {
        while (true)
        {
            if (StickmanTrigger && Character.GetCountOfList()>0)
            {
                GameObject go = Character.RemoveObjectFromList();
                go.transform.parent = transform;
                Vector3 height = Vector3.zero + Vector3.forward * ((cargoBoxList.Count + 1) * 0.035f);
                go.transform.DOMove(transform.position + Vector3.up * ((cargoBoxList.Count + 1) * 2), 0.3f);
                yield return new WaitForSeconds(0.3f);
                go.transform.DOComplete();
                cargoBoxList.Add(go);
                resetTheCargoPositions();
                bellScript.getActiveTheGiveCargo();
            }
            else
            {
                break;
            }
        }
    }

    void resetTheCargoPositions()
    {
        for (int i = 0; i < cargoBoxList.Count; i++)
        {
            cargoBoxList[i].transform.position = transform.position + Vector3.up * (i+1) * 2;
            cargoBoxList[i].transform.rotation = Quaternion.Euler(0,0,0);
        }
    }

    public GameObject takeBoxFromDesk()
    {
        GameObject cargoBox = cargoBoxList[cargoBoxList.Count - 1];
        cargoBoxList.RemoveAt(cargoBoxList.Count - 1);
        return cargoBox;
    }

    public int getCountOfList()
    {
        return cargoBoxList.Count;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StickmanTrigger = true;
            StartCoroutine(putBoxOnDesk());
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
