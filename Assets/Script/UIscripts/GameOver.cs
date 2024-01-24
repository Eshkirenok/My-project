using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Data;
using Mono.Data.Sqlite;

public class GameOver : MonoBehaviour
{
    private string playerName;
    private string connectionString;
   public void RestartGame()
    {
        SceneManager.LoadScene(2); // Загрузка сцены с игрой (сцена 2)
        Debug.Log("ЖиЖим");
         // Присвоить значение "1" полю Level в базе данных
       playerName = LoadPlayerData.PlayerInfo.name; // Получить имя игрока из PlayerPrefs

        // Создать подключение к базе данных
        connectionString = "URI=file:" + Application.dataPath + "/PlayersDatabase.sqlite";
        IDbConnection dbConnection = new SqliteConnection(connectionString);
        dbConnection.Open();



        // Обновить значение Level в базе данных
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "UPDATE Players SET Level = 1 WHERE Name = @Name";
        IDbDataParameter nameParameter = dbCommand.CreateParameter();
        nameParameter.ParameterName = "@Name";
        nameParameter.Value = playerName;
        dbCommand.Parameters.Add(nameParameter);
        dbCommand.ExecuteNonQuery();
    }
}
