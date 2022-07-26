using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public string PlayerName;
    public int HighScore;
    public string HighScoreName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadData();
    }

    [System.Serializable]
    class SaveInfo
    {
        public string PlayerName;
        public string HighScoreName;
        public int HighScore;
    }

    private string DataPath
    {
        get
        {
            return Application.persistentDataPath + "/savedata.json";
        }
    }

    public void ClearData()
    {
        if (File.Exists(DataPath))
        {
            File.Delete(DataPath);
            HighScoreName = "";
            HighScore = 0;
        }
    }

    public void SaveData()
    {
        SaveInfo info = new SaveInfo
        {
            PlayerName = PlayerName,
            HighScoreName = HighScoreName,
            HighScore = HighScore
        };
        string json = JsonUtility.ToJson(info);
        File.WriteAllText(DataPath, json);
    }

    public void LoadData()
    {
        if (File.Exists(DataPath))
        {
            string json = File.ReadAllText(DataPath);
            SaveInfo info = JsonUtility.FromJson<SaveInfo>(json);
            PlayerName = info.PlayerName;
            HighScoreName = info.HighScoreName;
            HighScore = info.HighScore;
        }
    }
}
