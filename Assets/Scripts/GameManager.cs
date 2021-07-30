using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public string PlayerName { get; set; }
    public float BallSpeed { get; set; }
    public int CurrentScore { get; set; }
    public ScoreEntry[] scoreTable { get; set; }

    public const string PATH_PLAYER = "player.json";
    public const string PATH_SETTINGS = "settings.json";
    public const string PATH_SCORETABLE = "scoretable.json";

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadPlayer();
            LoadSettings();
            InitScoreTable();
        }
        else
        {
            Destroy(gameObject);
        }

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SavePlayer()
    {
        PlayerData data = new PlayerData();
        data.name = PlayerName;

        SaveToFile(data, PATH_PLAYER);
    }

    public void LoadPlayer()
    {
        try
        {
            PlayerData data = LoadFromFile<PlayerData>(PATH_PLAYER);

            if (data.name != String.Empty)
            {
                PlayerName = data.name;
            }
            else
            {
                throw new Exception();
            }
        }
        catch
        {
            PlayerName = "Enter Your Name";
        }
    }

    public void SaveSettings()
    {
        SettingsData data = new SettingsData();
        data.ballSpeed = BallSpeed;

        SaveToFile(data, PATH_SETTINGS);
    }

    public void LoadSettings()
    {
        try
        {
            SettingsData data = LoadFromFile<SettingsData>(PATH_SETTINGS);
            
            if (data.ballSpeed >= 0.1f)
            {
                BallSpeed = data.ballSpeed;
            }
            else
            {
                throw new Exception();
            }
        }
        catch
        {
            BallSpeed = 1.0f;
        }
    }

    //public void UpdateScoreTable()
    //{
    //    LinkedListNode<(string name, int score)> pointer = scoreTable.First;

    //    while (pointer != null)
    //    {
    //        if (CurrentScore > pointer.Value.score)
    //        {
    //            scoreTable.AddBefore(pointer, (PlayerName, CurrentScore));
    //            scoreTable.RemoveLast();
    //        }
    //        else if ((CurrentScore == pointer.Value.score))
    //        {
    //            scoreTable.AddAfter(pointer, (PlayerName, CurrentScore));
    //            scoreTable.RemoveLast();
    //        }

    //        pointer = pointer.Next;
    //    }
    //}

    public void InitScoreTable()
    {
        scoreTable = new ScoreEntry[10];

        for (int i = 0; i < scoreTable.Length; i++)
        {
            scoreTable[i].name = "---";
            scoreTable[i].score = 0;
        }
    }

    public void SaveScoreTable()
    {
        ScoreTableData data = new ScoreTableData();

        for (int i = 0; i < 10; i++)
        {
            data.names[i] = scoreTable[i].name;
            data.scores[i] = scoreTable[i].score;
        }

        SaveToFile(data, PATH_SCORETABLE);
    }

    //public void LoadScoreTable()
    //{

    //}

    private void SaveToFile(object data, string PATH)
    {
        string json = JsonUtility.ToJson(data);

        File.WriteAllText($"{Application.persistentDataPath}/{PATH}", json);
    }

    private T LoadFromFile<T>(string PATH)
    {
        string path = $"{Application.persistentDataPath}/{PATH}";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            return JsonUtility.FromJson<T>(json);
        }
        else
        {
            throw new ArgumentException($"File `{PATH}` does not exist!");
        }
    }

    [Serializable]
    private class PlayerData
    {
        public string name;
    }

    [Serializable]
    private class SettingsData
    {
        public float ballSpeed;
    }

    [Serializable]
    private class ScoreTableData
    {
        public string[] names;
        public int[] scores;
    }

    public struct ScoreEntry
    {
        public string name;
        public int score;
    }
}
