using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoins : MonoBehaviour
{
    public static SpawnCoins instance;
    private float limitY = 3;
    private float limitX = 12;
    private float limitZ = 12;
    private Vector3 spawnPoint;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else instance = this;
    }

    void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            Invoke("SpawnObject", 0.5f);
        }
    }

    public void SpawnObject()
    {
        GameObject obj = ObjectPool.instance.GetObjectPooled();
        if (obj != null)
        {
            obj.transform.position = RandomSpawn();
            obj.SetActive(true);
        }
    }

    private Vector3 RandomSpawn()
    {
        spawnPoint.x = Random.Range(limitX, -limitX);
        spawnPoint.y = Random.Range(1, limitY);
        spawnPoint.z = Random.Range(limitZ, -limitZ);
        return spawnPoint;
    }
}
