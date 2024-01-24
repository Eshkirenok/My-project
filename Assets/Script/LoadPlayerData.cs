using UnityEngine;
using UnityEngine.UI;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadPlayerData : MonoBehaviour
{
    public GameObject myObject;
    public InputField nameInputField;
    private string playerName;
    private int coins;
    private int level;
    private int experience;
    string connectionString;

    public static class PlayerInfo
    {
        public static int coins;
        public static int level;
        public static int experience;
        public static string name;
    }

    public IEnumerator ActivateObjectFor4Seconds()
    {
        // Включить объект
        myObject.SetActive(true);

        // Подождать 4 секунды
        yield return new WaitForSeconds(4);

        // Выключить объект
        myObject.SetActive(false);
    }

    public void LoadPlayer()
    {
        // Получить имя игрока из поля ввода
        playerName = nameInputField.text;

        // Создать подключение к базе данных
        connectionString = "URI=file:" + Application.dataPath + "/PlayersDatabase.sqlite";
        IDbConnection dbConnection = new SqliteConnection(connectionString);
        dbConnection.Open();

        // Извлечь данные игрока из базы данных
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM Players WHERE Name=@Name";
        IDbDataParameter nameParameter = dbCommand.CreateParameter();
        nameParameter.ParameterName = "@Name";
        nameParameter.Value = playerName;
        dbCommand.Parameters.Add(nameParameter);
        IDataReader reader = dbCommand.ExecuteReader();
        Debug.Log("Загружен");

        // Проверить, есть ли игрок с таким именем в базе данных
        if (reader.Read())
        {
            PlayerInfo.coins = reader.GetInt32(reader.GetOrdinal("Coins"));
            PlayerInfo.level = reader.GetInt32(reader.GetOrdinal("Level"));
            PlayerInfo.experience = reader.GetInt32(reader.GetOrdinal("Experience"));
            PlayerInfo.name = playerName;

            // Закрыть подключение к базе данных
            reader.Close();
            dbConnection.Close();

            // Загрузить соответствующую сцену в зависимости от значения уровня
            switch (PlayerInfo.level)
            {
                case 1:
                    SceneManager.LoadScene(2);
                    break;
                case 2:
                    SceneManager.LoadScene(4);
                    break;
                case 3:
                    SceneManager.LoadScene(5);
                    break;
              //  case 0:
              //      SceneManager.LoadScene(3);
               //     break;
               // case 5:
              //      SceneManager.LoadScene(5);
              //      break;
                default:
                    Debug.Log("Недопустимый уровень: " + PlayerInfo.level);
                    break;
            }
        }
        else
        {
            // Закрыть подключение к базе данных
            reader.Close();
            dbConnection.Close();

            Debug.Log("Игрок с именем " + playerName + " не найден");
            StartCoroutine(ActivateObjectFor4Seconds());
        }
    }
}