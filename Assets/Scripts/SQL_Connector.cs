using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;
using System.Data;
using System.Threading;

[AddComponentMenu("_DeepLOM/SQL_Connector")]
public class SQL_Connector : MonoBehaviour
{
    public SqliteConnection connection { get; private set; }
    [SerializeField] private string dbname;
    private string path;

    private void Awake()
    {
        path = Application.dataPath;
    }

    private void Start()
    {
        SQL_Connect();
    }

    private void SQL_Connect()
    {
        path = Path.Combine(path, dbname);
        if (File.Exists(path))
        {
            connection = new SqliteConnection("URI=file:" + path);
            connection.Open();

            if (connection.State == ConnectionState.Open)
            {
                Debug.Log("SQL connection state: OK");
            }
            else
            {
                Debug.LogError($"SQL connection bad. State - {connection.State}");
            }
            GetAllScores();
        }
        else
        {
            Debug.LogError("Database not found");
        }
    }

    private void GetAllScores()
    {
        SqliteCommand cmd = new SqliteCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM users";
        SqliteDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            string res = string.Empty;
            foreach(var row in reader)
            {
                res += row.ToString() + " ";
            }
            Debug.LogWarning(res);
        }
    }

    private void CreateNewUser(string username = "Иванов Иван Иванович")
    {
        SqliteCommand cmd = new SqliteCommand();
        cmd.Connection = connection;
        cmd.CommandText = $"INSERT INTO users(name) VALUES ('{username}')";
        SqliteDataReader reader = cmd.ExecuteReader();
        reader.Close();
    }

    private void OnDestroy()
    {
        connection.Close();
    }
}
