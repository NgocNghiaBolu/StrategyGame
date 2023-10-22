using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject EnemyPrefabs;
    [SerializeField] [Range(0.1f,30f)] float waitTime = 1.5f;
    [SerializeField] [Range(0,50)] int poolSize = 5;

    GameObject[] pool;

    private void Awake()
    {
        PopulaPool();
    }

    void Start()
    {
        StartCoroutine(SpawEnmy());
    }

    void PopulaPool()
    {
        pool = new GameObject[poolSize];//gan pool bang poolsize
        for(int i = 0;i < pool.Length; i++)
        {
            pool[i] = Instantiate(EnemyPrefabs, transform.position,Quaternion.identity);
            pool[i].transform.parent = transform;
            pool[i].SetActive(false);
        }
    }

    void EnableObjectInPool()
    {
        for(int i = 0; i < pool.Length; i++)
        {
            if (pool[i].activeInHierarchy == false)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }

    IEnumerator SpawEnmy()
    {
        while (true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(waitTime);
        }
    }
}
