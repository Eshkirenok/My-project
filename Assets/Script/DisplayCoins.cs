using UnityEngine;
using UnityEngine.UI;
using System.Data;
using Mono.Data.Sqlite;

public class DisplayCoins : MonoBehaviour
{
    public Text coinsText;

    void Update()
    {
        string conn = "URI=file:" + Application.dataPath + "/PlayersDatabase.sqlite"; // Путь к файлу базы данных
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); // Открыть соединение с базой данных

        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT coins FROM Players WHERE name = '" + LoadPlayerData.PlayerInfo.name + "'";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        if (reader.Read())
        {
            int coins = reader.GetInt32(0);
            coinsText.text = "Coins: " + coins.ToString();
            Debug.Log("Coins: " + coins.ToString());
        }

        reader.Dispose();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;

        dbconn.Close();
        dbconn = null;
    }
}