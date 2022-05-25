using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;
using System.Data;
using System;

[AddComponentMenu("_DeepLOM/SQL_Connector")]
public class SQL_Connector : MonoBehaviour
{
    public SqliteConnection connection { get; private set; }
    [SerializeField] private string dbname;
    private string path;
    public List<User> users { get; private set; }
    public List<Score> scores { get; private set;}

    private void Awake()
    {
        path = Application.dataPath;
        users = new List<User>(20);
        scores = new List<Score>(100);
    }

    private void Start()
    {
        SQL_Connect();

        if (connection.State == ConnectionState.Open)
        {
            ReceiveAllUsers();
            ReceiveAllScores();
        }
    }

    public string[] GetScoreTableLines()
    {
        string[] result = new string[scores.Count]; 
        for (int i = 0; i < scores.Count; i++)
        {
            Score score = scores[i];
            User currUser = GetUserById(score.user);
            result[i] = ($"{score.id}) user: {currUser.name}, score: {score.score}");
        }
        return result;
    }

    public User GetUserById(int id)
    {
        foreach (User user in users)
        {
            if (user.id == id)
            {
                return user;
            }
        }
        return null;
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
        }
        else
        {
            Debug.LogError("Database not found");
        }
    }

    private void ReceiveAllScores()
    {
        SqliteCommand cmd = new SqliteCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM results";
        SqliteDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            try
            {
                Score score = new Score()
                {
                    score = double.Parse(reader["score"].ToString()),
                    user = int.Parse(reader["user"].ToString()),
                    id = int.Parse(reader["id"].ToString()),
                };
                scores.Add(score);
            }
            catch (Exception e)
            {
                Debug.LogError($"Can't convert scores: {e.Message}");
            }
        }
        reader.Close();
    }

    private void ReceiveAllUsers()
    {
        SqliteCommand cmd = new SqliteCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM users";
        SqliteDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            try
            {
                User user = new User()
                {
                    id = int.Parse(reader["id"].ToString()),
                    name = reader["name"].ToString(),
                };
                users.Add(user);
            }
            catch(Exception e)
            {
                Debug.LogError($"Can't convert users: {e.Message}");
            }
        }
        reader.Close();    
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

public class User
{
    public string name { get; set; }
    public int id { get; set; }
}

public class Score
{
    public double score { get; set; }
    public int id { get; set; }
    public int user { get; set; }
}
