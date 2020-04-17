using UnityEngine;

public class SpawnController : MonoBehaviour
{

    private Pool pool;
    public Pool Pool
    {
        get { return pool; }
        set
        {
            if (pool == null)
                pool = value;
            else
                throw new System.Exception("Error");
        }
    }


    [SerializeField]
    private GameObject[] spawnObjects;

    [SerializeField]
    private float spawnRate = 1f;

    [SerializeField]
    private float firstSpawnDelay = 0f;

    [SerializeField]
    private Player player;

    private Vector3 spawnPoint;



    private bool IsThereAtLeastOneObjectToSpawn
    {
        get
        {
            bool result = false;

            for (int i = 0; i < spawnObjects.Length; i++)
            {
                result = spawnObjects[i] != null;

                if (result)
                {
                    break;
                }
            }

            return result;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (spawnObjects.Length > 0 && IsThereAtLeastOneObjectToSpawn)
        {
            InvokeRepeating("SpawnObject", firstSpawnDelay, spawnRate);

            if (player != null)
            {
                player.OnPlayerDied += StopSpawning;
            }
        }
    }

    private void SpawnObject()
    {
        GameObject spawnGO = spawnObjects[Random.Range(0, spawnObjects.Length)];

        if (spawnGO != null)
        {
            spawnPoint = Camera.main.ViewportToWorldPoint(new Vector3(
                Random.Range(0F, 1F), 1F, transform.position.z));

            GameObject instance = Instantiate(spawnGO, spawnPoint, Quaternion.identity);
            Pool.ReturnToPool(gameObject);
        }
        
    }

    private void StopSpawning()
    {
        CancelInvoke();
    }

    internal interface IGameObjectPooled
    {
        Pool Pool { get; set; }
    }
}