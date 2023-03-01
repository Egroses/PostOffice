using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackPlace : MonoBehaviour
{
    //int a = 0;
    //[SerializeField] float StackTime;
    [SerializeField] CharObjectsList Character;
    [SerializeField] string nameOfPoolObject;
    [SerializeField] List<GameObject> StackableObject=new List<GameObject>();
    [SerializeField] List<GameObject> StackPlaceVector3;
    bool StickmanTrigger=false;
    Coroutine controlTakeBox,controlSpawnBox;
    void Start() 
    {
        controlTakeBox = null;
        controlSpawnBox = null;
    }

    void Update()
    {
        if (StickmanTrigger && controlTakeBox == null)
        {
            controlTakeBox = StartCoroutine(takeBoxObjectFromPlace());
        }
        if (StackableObject.Count < 9 && controlSpawnBox == null)
        {
            controlSpawnBox = StartCoroutine(spawnBoxObject());
        }
    }
    IEnumerator spawnBoxObject()
    {
        while (StackableObject.Count < 9)
        {
            GameObject go = ObjectPool.Instance.GetFromPool(nameOfPoolObject);
            go.SetActive(true);
            go.transform.position = StackPlaceVector3[StackableObject.Count].transform.position;
            StackableObject.Add(go);
            yield return new WaitForSeconds(1f);
        }
        controlSpawnBox = null;
    }
    IEnumerator takeBoxObjectFromPlace()
    {
        for (int i = 0; i < StackableObject.Count; i++)
        {
            if (StickmanTrigger)
            {
                int a = StackableObject.Count - 1;
                GameObject go = StackableObject[a];
                StackableObject.RemoveAt(a);
                Character.AddObjectToList(go);

                yield return new WaitForSeconds(0.3f);
            }

        }
        controlTakeBox = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("stickman"))
        {
            StickmanTrigger = true;
            //StackTime = 1f;
        }
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    if (StickmanTrigger && StackableObject.Count>0 && StackTime<0)
    //    {
    //        Debug.Log(StackTime);
    //        int a = StackableObject.Count - 1;
    //        GameObject go = StackableObject[a];
    //        StackableObject.RemoveAt(a);
    //        Character.addObjectToList(go);
    //        StackTime = 3f;
    //    }
    //    StackTime -= Time.deltaTime;
    //}
    private void OnTriggerExit(Collider other)
    {
        if (other.name.Equals("stickman"))
        {
            StickmanTrigger=false;
        }
    }
}
