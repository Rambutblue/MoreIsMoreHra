using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] playerPrefabs;
    [SerializeField]
    private GameObject[] vehiclePrefabs;
    private List<float> lines = new List<float>();
    private float storeX;
    private float spawnDelay = 0;
    private float spawnTime;

    public List<GameObject> pooledObjectsLight = new List<GameObject>();
    public List<GameObject> pooledObjectsMedium = new List<GameObject>();
    private List<GameObject> pooledObjectsHeavy = new List<GameObject>();
    [SerializeField]
    private int amountToPool;
    private void Awake()
    {
        lines.Add(-9f);
        lines.Add(0f);
        lines.Add(9f);
    }

    // Start is called before the first frame update
    void Start()
    {
        // ruzny auta pridat
        Instantiate(playerPrefabs[0], Vector3.zero, playerPrefabs[0].transform.rotation);

        PoolObjects(pooledObjectsLight, vehiclePrefabs[0]);
        PoolObjects(pooledObjectsMedium, vehiclePrefabs[1]);
        PoolObjects(pooledObjectsHeavy, vehiclePrefabs[2]);
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
        vehicle.SetActive(true);
        storeX = lines[x];
        lines.Remove(lines[x]);
        spawnDelay = Random.Range(0.5f, 1);
    }
    GameObject GetPooledObject()
    {
        List<GameObject> listObj = null;
        int x = Random.Range(0, 3);
        switch (x)
        {
            case 0:
                listObj = pooledObjectsLight;
                break;
            case 1:
                listObj = pooledObjectsMedium;
                break;
            case 2:
                listObj = pooledObjectsHeavy;
                break;
        }
        for (int i = 0; i < listObj.Count; i++)
        {
            if (!listObj[i].activeInHierarchy)
            {
                return listObj[i];
            }
        }
        return null;
    }
    void PoolObjects(List<GameObject> list, GameObject vehicle)
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(vehicle);
            obj.SetActive(false);
            list.Add(obj);
            obj.transform.SetParent(this.transform);
        }
    }
    public void GameOver()
    {

    }
}
