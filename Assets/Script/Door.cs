using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Data;
using Mono.Data.Sqlite;

public class Door : MonoBehaviour
{
    public GameObject interactButton;

    private string playerName;
    private string connectionString;

    private void Start()
    {
        interactButton = GameObject.Find("InteractButton");
        interactButton.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Rogue" || other.name == "Wizard" || other.name == "Knight")
        {
            interactButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Rogue" || other.name == "Wizard" || other.name == "Knight")
        {
            interactButton.SetActive(false);
        }
    }

    public void Interact()
    {
        interactButton.SetActive(false);
        
        // Загрузить новую сцену
        SceneManager.LoadScene(4);

        // Присвоить значение "1" полю Level в базе данных
       playerName = LoadPlayerData.PlayerInfo.name; // Получить имя игрока из PlayerPrefs

        // Создать подключение к базе данных
        connectionString = "URI=file:" + Application.dataPath + "/PlayersDatabase.sqlite";
        IDbConnection dbConnection = new SqliteConnection(connectionString);
        dbConnection.Open();



        // Обновить значение Level в базе данных
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "UPDATE Players SET Level = 2 WHERE Name = @Name";
        IDbDataParameter nameParameter = dbCommand.CreateParameter();
        nameParameter.ParameterName = "@Name";
        nameParameter.Value = playerName;
        dbCommand.Parameters.Add(nameParameter);
        dbCommand.ExecuteNonQuery();

        // Закрыть подключение к базе данных
        dbConnection.Close();
    }
}