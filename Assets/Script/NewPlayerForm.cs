using UnityEngine;
using UnityEngine.UI;
using System.Data;
using Mono.Data.Sqlite;
using System.Collections;
public class NewPlayerForm : MonoBehaviour
{
    private string playerName;
    public GameObject myObject2;
    private string connectionString;
    public InputField nameInputField;
        public IEnumerator ActivateObjectFor4Seconds2()
        {
            // Включить объект
            myObject2.SetActive(true);

            // Подождать 4 секунды
            yield return new WaitForSeconds(2);

            // Выключить объект
            myObject2.SetActive(false);
        }

    public void SaveNewPlayer()
    {
        // Получить имя игрока из формы
        playerName = nameInputField.text;

        // Создать подключение к базе данных
        
        connectionString = "URI=file:C:/Users/olein/My project/Assets/PlayersDatabase.sqlite";
        //connectionString = "URI=file:" + Application.dataPath + "/PlayersDatabase.sqlite";
        IDbConnection dbConnection = new SqliteConnection(connectionString);
        dbConnection.Open();

        // Создать запись в базе данных для нового игрока
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "INSERT INTO Players(Name, Coins, Level, Experience) VALUES (@Name, @Coins, @Level, @Experience)";
        IDbDataParameter nameParameter = dbCommand.CreateParameter();
        nameParameter.ParameterName = "@Name";
        nameParameter.Value = playerName;
        dbCommand.Parameters.Add(nameParameter);
        IDbDataParameter coinsParameter = dbCommand.CreateParameter();
        coinsParameter.ParameterName = "@Coins";
        coinsParameter.Value = 0;
        dbCommand.Parameters.Add(coinsParameter);
        IDbDataParameter levelParameter = dbCommand.CreateParameter();
        levelParameter.ParameterName = "@Level";
        levelParameter.Value = 1;
        dbCommand.Parameters.Add(levelParameter);
        IDbDataParameter experienceParameter = dbCommand.CreateParameter();
        experienceParameter.ParameterName = "@Experience";
        experienceParameter.Value = 0;
        dbCommand.Parameters.Add(experienceParameter);
        dbCommand.ExecuteNonQuery();

        
        StartCoroutine(ActivateObjectFor4Seconds2());


        // Закрыть подключение к базе данных
        dbConnection.Close();
    }
}