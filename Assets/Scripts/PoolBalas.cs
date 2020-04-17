using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBalas : MonoBehaviour
{
    public PoolBalas Instance { get; private set; }

    public GameObject prefabs;

    List<GameObject> pool2 = new List<GameObject>();

    public void Awake()
    {
        Instance = this;
        FillPool();
    }

    void FillPool()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject go = Instantiate(prefabs);
            go.SetActive(false);

            pool2.Add(go);
        }
    }

    public GameObject Get()
    {
        GameObject ret;
        if (pool2.Count > 0)
        {
            ret = pool2[pool2.Count - 1];
            pool2.RemoveAt(pool2.Count - 1);
        }
        else
        {
            ret = Instantiate(prefabs);

        }
        ret.SetActive(true);

        return ret;
    }

    public void Return (GameObject go)
        {
        go.SetActive(false);
        pool2.Add(go);
        }

}
