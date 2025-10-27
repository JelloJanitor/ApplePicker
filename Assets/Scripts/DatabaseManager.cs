using System;
using System.IO;
using SQLite;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    private static DatabaseManager _instance;
    public static DatabaseManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("DatabaseManager");
                _instance = obj.AddComponent<DatabaseManager>();
                DontDestroyOnLoad(obj);
            }

            return _instance;
        }
    }

    public PlayerData player;
    public string dbPath;
    public SQLiteConnection dbConnection;

    private int dummyHighScore = 0;

    private void Start()
    {
        dbPath = Path.Combine(Application.persistentDataPath, "GameData.db");
        dbConnection = new SQLiteConnection(dbPath);
        dbConnection.CreateTable<PlayerData>();
        LoadPlayerData();
    }

    private void LoadPlayerData()
    {
        player = dbConnection.Table<PlayerData>().OrderByDescending(p=>p.HighScore).FirstOrDefault();
    }

    private void InsertPlayerData()
    {
        if (player == null)
        {
            player = new PlayerData
            {
                PlayerName = "Player1",
                HighScore = dummyHighScore,
                DateAchieved = "WHATEVS" // FIX
            };
        }
    }
}
