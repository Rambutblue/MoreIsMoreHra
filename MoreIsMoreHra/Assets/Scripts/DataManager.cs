using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public int bestSessionScore;
    public int bestScore;
    public int charType;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadData();
    }
    private void Start()
    {
        
    }
    class SavedData
    {
        public int bestScore;
    }
    public void SaveData()
    {
        if (bestSessionScore > bestScore)
        {
            bestScore = bestSessionScore;
            SavedData data = new SavedData();
            data.bestScore = bestScore;

            string json = JsonUtility.ToJson(data);

            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }
        
    }
    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SavedData data = JsonUtility.FromJson<SavedData>(json);

            bestScore = data.bestScore;
        }

    }
}
