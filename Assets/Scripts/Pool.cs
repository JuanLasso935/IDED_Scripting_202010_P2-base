using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    [SerializeField]

    private GameObject prefab2;

    [SerializeField]
    private GameObject prefab3;

    public static Pool Instance { get; private set; }
    private Queue<GameObject> objects = new Queue<GameObject>();


    private void Awake()
    {
        Instance = this;
    }


    public GameObject Get()
    {
        if (objects.Count == 0)
            AddObjects(1);

        return objects.Dequeue();
    }

    public void ReturnToPool(GameObject objectToReturn)
    {
        objectToReturn.SetActive(false);
        objects.Enqueue(objectToReturn);
    }

    private void AddObjects(int count)
    {
        var newObject = GameObject.Instantiate(prefab);

        newObject.SetActive(false);
        objects.Enqueue(newObject);


       newObject.GetComponent<SpawnController.IGameObjectPooled>().Pool = this;
    }

}
