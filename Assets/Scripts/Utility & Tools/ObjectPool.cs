using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    private List<GameObject> objs = new List<GameObject>();

    private void Start()
    {
        for(int i=0; i<3; i++)
        {
            CreateNewObject();
        }
    }

    public GameObject GetObject(bool active)
    {
        foreach(GameObject obj in objs)
        {
            if(!obj.activeInHierarchy)
            {
                obj.SetActive(active);
                return obj;
            }
        }
        GameObject newObj = CreateNewObject();
        newObj.SetActive(active);
        return newObj;
    }

    private GameObject CreateNewObject()
    {
        GameObject newObj = Instantiate(prefab, transform);
        newObj.SetActive(false);
        objs.Add(newObj);
        return newObj;
    }
}
