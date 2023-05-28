using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    public List<GameObject> pooledObjects;
    public GameObject prefab;
    public int amountPool = 3;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject obj;
        for (int i = 0; i < amountPool; i++)
        {
            obj = Instantiate(prefab);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetObjectPooled()
    {
        for (int i = 0; i < amountPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
