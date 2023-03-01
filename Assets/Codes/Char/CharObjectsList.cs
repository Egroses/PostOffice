using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharObjectsList : MonoBehaviour
{
    [SerializeField] List<GameObject> listOfObjects;
    [SerializeField] GameObject referenceStack;
    [SerializeField] float heightValue;
    private float _height = 0;
    private void Start()
    {
        referenceStack = transform.Find("ReferencePoint").gameObject;
    }
    public void AddObjectToList(GameObject addableObject)
    {
        _height = listOfObjects.Count * 2*heightValue;
        listOfObjects.Add(addableObject);
        addableObject.transform.parent = referenceStack.transform;
        addableObject.GetComponent<Rigidbody>().isKinematic = true;//objelere on of acilabilir
        addableObject.GetComponent<Rigidbody>().useGravity = false;
        addableObject.GetComponent<Collider>().enabled = false;
        StartCoroutine(SetPositionTrue(addableObject,_height));
    }
    IEnumerator SetPositionTrue(GameObject addableObject,float upHeight)
    {
        var position = referenceStack.transform.position;
        addableObject.transform.DOMove(position + Vector3.up * upHeight, 0.3f);
        yield return new WaitForSeconds(0.3f);
        addableObject.transform.position= position + Vector3.up * upHeight;
        addableObject.transform.rotation = transform.rotation;
    }

    public GameObject RemoveObjectFromList()
    {
        GameObject cargoBox = listOfObjects[listOfObjects.Count - 1];
        listOfObjects.RemoveAt(listOfObjects.Count - 1);
        return cargoBox;
    }
    public int GetCountOfList()
    {
        return listOfObjects.Count;
    }
}
