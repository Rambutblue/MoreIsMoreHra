using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] playerPrefabs;
    [SerializeField]
    private GameObject[] vehiclePrefabs;
    [SerializeField]
    private GameObject[] powerupPrefabs;
    private List<Renderer> powerupRenderers = new List<Renderer>();
    private List<float> lines = new List<float>();
    private float storeX;

    private float spawnDelay = 0.5f;
    public float spawnTime;

    public float gameSpeed { get; private set; }
    private float gameScoreFloat;
    public int gameScore { get; private set; }
    [SerializeField]
    private float gameSpeedMultiplier;
    [SerializeField]
    private float gameScoreMultiplier;
    public GameObject scoreGameObj;

    [SerializeField]
    private int _playerHp;
    public int playerHp
    {
        get { return _playerHp; }
        set {
            if (value < 0)
            {
                _playerHp = 0;
            }
            else
            {
                _playerHp = value;
            }
        }
    }
    public int playerDmg;

    private List<GameObject> pooledObjectsLight = new List<GameObject>();
    private List<GameObject> pooledObjectsMedium = new List<GameObject>();
    private List<GameObject> pooledObjectsHeavy = new List<GameObject>();
    private List<GameObject> pooledObjectsPowerups = new List<GameObject>();
    [SerializeField]
    private int amountToPool;
    [SerializeField]
    private float SpawnHeight = 10;
    private Transform HpParentTransform;
    private List<GameObject> HpUI = new List<GameObject>();

    public GameObject GameOverMenu;

    public bool isPowerupActive;

    private void Awake()
    {
        lines.Add(-9f);
        lines.Add(0f);
        lines.Add(9f);
        gameSpeed = 20;
        Time.timeScale = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        Instantiate(playerPrefabs[DataManager.instance.charType], Vector3.zero, playerPrefabs[DataManager.instance.charType].transform.rotation);

        PoolObjects(pooledObjectsLight, vehiclePrefabs[0]);
        PoolObjects(pooledObjectsMedium, vehiclePrefabs[1]);
        PoolObjects(pooledObjectsHeavy, vehiclePrefabs[2]);
        PoolPowerups();

        StartCoroutine(LateStart());
    }

    private IEnumerator LateStart()
    {
        yield return new WaitForEndOfFrame();
        SetHpUI();
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime += Time.deltaTime;
        if (spawnTime > spawnDelay)
        {
            Spawn();
            spawnTime = 0;
        }
        gameSpeed += gameSpeedMultiplier * Time.deltaTime;
        gameScoreFloat += Time.deltaTime * gameScoreMultiplier;
        gameScore = Mathf.RoundToInt(gameScoreFloat);
        scoreGameObj.GetComponent<TextMeshProUGUI>().text = gameScore.ToString();
    }

    void Spawn()
    {
        //lines.Add(storeX);
        int x = Random.Range(0, lines.Count);
        GameObject vehicle = GetPooledObject();
        vehicle.transform.position = new Vector3(lines[x], SpawnHeight, 55);
        //storeX = lines[x];
        //lines.Remove(lines[x]);
        spawnDelay = Random.Range(0.5f, 1);
    }
    GameObject GetPooledObject()
    {
        List<GameObject> listObj = null;
        int x = Random.Range(0, 35);
        if (x == 0 && !isPowerupActive)
        {
            listObj = pooledObjectsPowerups;
            int y = Random.Range(0, listObj.Count);
            Transform poweruptransform = listObj[y].GetComponent<Transform>();
            PowerupRendererControl(true, poweruptransform);
            isPowerupActive = true;
            return listObj[y];
        }
        else
        {
            x = Random.Range(0, 3);
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
                    listObj[i].SetActive(true);
                    return listObj[i];
                }
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

    void PoolPowerups()
    {
        for (int i = 0; i < powerupPrefabs.Length; i++)
        {
            GameObject obj = (GameObject)Instantiate(powerupPrefabs[i], new Vector3(0,0,-40), powerupPrefabs[i].transform.rotation, this.transform);
            Transform poweruptransform = obj.GetComponent<Transform>();
            PowerupRendererControl(false, poweruptransform);
            pooledObjectsPowerups.Add(obj);
        }
    }

    public void PowerupRendererControl(bool activate, Transform transform)
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Renderer>().enabled = activate;
        }
    }

    public void PlayerHit()
    {
        playerHp -= playerDmg;
        UpdateHp();
        if (playerHp == 0)
        {
            GameOver();
        }
        else
        {
            InactivateAllObstacles();
            spawnTime = -0.5f;
        }
    }
    public void InactivateAllObstacles()
    {
        InactivateAllObstaclesOfType(pooledObjectsLight);
        InactivateAllObstaclesOfType(pooledObjectsMedium);
        InactivateAllObstaclesOfType(pooledObjectsHeavy);
    }
    private void GameOver()
    {
        SaveRunScore();
        DataManager.instance.SaveData();
        Time.timeScale = 0;
        GameOverMenu.SetActive(true);
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
    public void SaveRunScore()
    {
        DataManager.instance.bestSessionScore = gameScore;
    }

}
