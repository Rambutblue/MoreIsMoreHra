using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    private int bestSessionScore;
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
    }
    class SavedData
    {
        public int bestScore;
    }
    public void SaveData()
    {
        if (bestSessionScore > bestScore)
        {
            SavedData data = new SavedData();
            data.bestScore = bestScore;

            string json = JsonUtility.ToJson(data);

            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
            Debug.Log(Application.persistentDataPath + "/savefile.json");
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
