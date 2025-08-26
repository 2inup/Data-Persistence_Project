using UnityEngine;
using System.IO;
using System;


public class DataManager : MonoBehaviour
{


    public static DataManager Instance;
    public string UserName = "";
    public int BestScore = 0;
    public string BestUserName = "";


    [System.Serializable]
    private class SaveData
    {
        public string bestUserName;
        public int bestScore;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadBestScore();
    }

    public void SetCurrentPlayerName(string userName)
    {
        UserName = userName;
    }

    public void ChangeBestScore(int score)
    {
        if (score > BestScore)
        {
            BestScore = score;
            BestUserName = string.IsNullOrEmpty(UserName) ? "Player" : UserName;
            SaveBestScore();
        }
    }

    public void SaveBestScore()
    {
        SaveData data = new SaveData();
        {
            data.bestScore = BestScore;
            data.bestUserName = BestUserName;
        }

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(GetSavePath(), json);

    }
    public void LoadBestScore()
    {
        string path = GetSavePath();
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            BestScore = data.bestScore;
            BestUserName = data.bestUserName;
            }
    }

    private string GetSavePath()
    {
        return Application.persistentDataPath + "/savefile.json";
    }
}
