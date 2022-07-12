using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] playerPrefabs;
    [SerializeField]
    private GameObject vehiclePrefab;
    private List<float> lines = new List<float>();
    private float storeX;
    private float spawnDelay = 0;
    private float spawnTime;

    public List<GameObject> pooledObjects;
    public int amountToPool;
    private void Awake()
    {
        lines.Add(-7.5f);
        lines.Add(0f);
        lines.Add(7.5f);
    }

    // Start is called before the first frame update
    void Start()
    {
        // ruzny auta pridat
        Instantiate(playerPrefabs[0], Vector3.zero, playerPrefabs[0].transform.rotation);

        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(vehiclePrefab);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            obj.transform.SetParent(this.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime += Time.deltaTime;
        if (spawnTime > spawnDelay)
        {
            VehicleSpawn();
            spawnTime = 0;
        }
    }

    void VehicleSpawn()
    {
        lines.Add(storeX);
        int x = Random.Range(0, lines.Count);
        GameObject vehicle = GetPooledObject();
        vehicle.transform.position = new Vector3(lines[x], 0, 55);
        storeX = lines[x];
        lines.Remove(lines[x]);
        spawnDelay = Random.Range(0.5f, 1);
    }
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
