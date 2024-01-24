using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;

public class Coin : MonoBehaviour
{
    private string playerName;
    private string connectionString;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Получить имя игрока из скрипта загрузки игрока
            playerName = LoadPlayerData.PlayerInfo.name;

            // Создать подключение к базе данных
            connectionString = "URI=file:" + Application.dataPath + "/PlayersDatabase.sqlite";
            IDbConnection dbConnection = new SqliteConnection(connectionString);
            dbConnection.Open();

            // Увеличить количество монет у игрока с указанным именем в базе данных
            IDbCommand dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = "UPDATE Players SET Coins = Coins + 1 WHERE Name=@Name";
            IDbDataParameter nameParameter = dbCommand.CreateParameter();
            nameParameter.ParameterName = "@Name";
            nameParameter.Value = playerName;
            dbCommand.Parameters.Add(nameParameter);
            dbCommand.ExecuteNonQuery();

            // Закрыть подключение к базе данных
            dbConnection.Close();

            // Уничтожить монетку
            Destroy(gameObject);
        }
    }
}