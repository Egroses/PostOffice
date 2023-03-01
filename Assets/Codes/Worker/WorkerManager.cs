using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerManager : MonoBehaviour
{
    [SerializeField] string workerName;

    List<WorkerScript> workerScripts = new List<WorkerScript>();
    int id = 0;
    public void recruitmentWorker()
    {
        GameObject worker = ObjectPool.Instance.GetFromPool(workerName);
        worker.transform.position = transform.position;
        worker.SetActive(true);
        workerScripts.Add(worker.GetComponent<WorkerScript>());
        workerScripts[workerScripts.Count - 1].setId(id);
        id++;
        GameEvents.current.WorkerTakeCargo(workerScripts[workerScripts.Count - 1].getId());
    }
}
