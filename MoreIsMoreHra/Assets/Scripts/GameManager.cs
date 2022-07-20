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
    public int playerHp;
    public int playerDmg;

    private List<GameObject> pooledObjectsLight = new List<GameObject>();
    private List<GameObject> pooledObjectsMedium = new List<GameObject>();
    private List<GameObject> pooledObjectsHeavy = new List<GameObject>();
    [SerializeField]
    private int amountToPool;
    [SerializeField]
    private float SpawnHeight = 10;
    [SerializeField]
    private Transform HpParentTransform;
    private List<GameObject> HpUI = new List<GameObject>();
    private void Awake()
    {
        lines.Add(-9f);
        lines.Add(0f);
        lines.Add(9f);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetHpUI();
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
        vehicle.transform.position = new Vector3(lines[x], SpawnHeight, 55);
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

    public void PlayerHit()
    {
        playerHp -= playerDmg;
        UpdateHp();
        if (playerHp <= 0)
        {
            GameOver();
        }
        else
        {
            InactivateAllObstaclesOfType(pooledObjectsLight);
            InactivateAllObstaclesOfType(pooledObjectsMedium);
            InactivateAllObstaclesOfType(pooledObjectsHeavy);
            spawnTime = -1;
        }
    }
    private void GameOver()
    {
        Debug.Log("GameOver");
        Time.timeScale = 0;
        
    }
    private void InactivateAllObstaclesOfType(List<GameObject> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            list[i].SetActive(false);
        }
    }
    private void SetHpUI()
    {
        HpParentTransform = GameObject.Find("Hp").GetComponent<Transform>();
        foreach (Transform child in HpParentTransform)
        {
            HpUI.Add(child.gameObject);
        }
        for (int i = 0; i < playerHp; i++)
        {
            HpUI[i].SetActive(true);
        }
    }
    private void UpdateHp()
    {
        for (int i = HpUI.Count - 1; i > playerHp - 1; i--)
        {
            HpUI[i].SetActive(false);
        }
    }
}
