using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DataBase : MonoBehaviour
{
    public InputField loginA;
    public InputField passwordA;
    public GameObject Menu;
    public GameObject Game;
    public GameObject panel;

    public void OnButtonClick()
    {
        string NewLogin = loginA.text;
        string NewPassword = passwordA.text;
        if (NewPassword.Length > 3)
        {
            string conn = "URI=file:" + Application.dataPath + "/testdb.db";
            IDbConnection dbconn;
            dbconn = (IDbConnection)new SqliteConnection(conn);
            dbconn.Open();
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "INSERT INTO User (username, password, coins, level) VALUES('" + NewLogin + "', '" + NewPassword + "', 0, 1)";
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            dbconn = null;
        }
        else
        {
            Debug.Log("Password must be longer than 3 characters!");
        }    
    }

    public void OnButtonClick2()
    {
        string conn = "URI=file:" + Application.dataPath + "/testdb.db";
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open();
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT username,password, coins, level " + "FROM User";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            string Login = loginA.text;
            string Pass = passwordA.text;
            string username = reader.GetString(0);
            string password = reader.GetString(1);
            int coins = reader.GetInt32(2);
            int level = reader.GetInt32(3);
            if (Login == username && Pass == password)
            {
                SceneManager.LoadScene(2);
                break;
            }
            else 
            {
                Debug.Log("Incorrect login or password!");
            }
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }

    public void UpdateCoins(int coins)
{
    string conn = "URI=file:" + Application.dataPath + "/testdb.db";
    IDbConnection dbconn;
    dbconn = (IDbConnection)new SqliteConnection(conn);
    dbconn.Open();
    IDbCommand dbcmd = dbconn.CreateCommand();

    string login = loginA.text;
    string sqlQuery = "SELECT coins FROM User WHERE username = '" + login + "'";
    dbcmd.CommandText = sqlQuery;
    IDataReader reader = dbcmd.ExecuteReader();
    while (reader.Read())
    {
        int currentCoins = reader.GetInt32(0);
        currentCoins += coins;
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;

        IDbCommand dbcmd2 = dbconn.CreateCommand();
        string sqlQuery2 = "UPDATE User SET coins = " + currentCoins + " WHERE username = '" + login + "'";
        dbcmd2.CommandText = sqlQuery2;
        dbcmd2.ExecuteNonQuery();

        break;
    }

    dbconn.Close();
    dbconn = null;
}
    public static int GetCoins()
{
    int coins = 0;

    string conn = "URI=file:" + Application.dataPath + "/testdb.db";
    IDbConnection dbconn;
    dbconn = (IDbConnection)new SqliteConnection(conn);
    dbconn.Open();
    IDbCommand dbcmd = dbconn.CreateCommand();
    string sqlQuery = "SELECT coins FROM User";
    dbcmd.CommandText = sqlQuery;
    IDataReader reader = dbcmd.ExecuteReader();
    while (reader.Read())
    {
        coins = reader.GetInt32(0);
    }
    reader.Close();
    reader = null;
    dbcmd.Dispose();
    dbcmd = null;
    dbconn.Close();
    dbconn = null;

    return coins;
}
}