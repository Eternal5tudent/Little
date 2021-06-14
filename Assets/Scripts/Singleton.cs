using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: Component
{
    public static T Instance { get; private set; }
    protected virtual void Awake()
    {
        if(Instance == null)
            Instance = GetComponent<T>();
        else
        {
            Debug.Log("Found another gameObject with the same script: " + this.name + "Destroying the first one and replacing it...");
            Destroy(Instance.gameObject);
            GameObject obj = new GameObject();
            T comp = obj.AddComponent<T>();
            Instance = comp;
        }
    }  
}
