using System;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public string CurrentPlayerName = "";
    public int BestScore = 0;
    public string BestScorePlayerName = "";

    [Serializable]
    private class SaveData
    {
        public int bestScore;
        public string bestScorePlayerName;
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

    public void SetPlayerName(string playerName)
    {
        CurrentPlayerName = playerName;
    }

    public void TrySaveBestScore(int score)
    {
        if (score > BestScore)
        {
            BestScore = score;
            BestScorePlayerName = string.IsNullOrEmpty(CurrentPlayerName) ? "Player" : CurrentPlayerName;
            SaveBestScore();
        }
    }

    public void SaveBestScore()
    {
        SaveData data = new SaveData
        {
            bestScore = BestScore,
            bestScorePlayerName = BestScorePlayerName
        };

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
            BestScorePlayerName = data.bestScorePlayerName;
        }
    }

    private string GetSavePath()
    {
        return Path.Combine(Application.persistentDataPath, "savefile.json");
    }
}


