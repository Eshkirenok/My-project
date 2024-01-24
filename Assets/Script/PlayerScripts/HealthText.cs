using UnityEngine;
using UnityEngine.UI;

public class HealthText : MonoBehaviour
{
    public PlayerInteraction player;

    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
       text.text = "HP: " + player.health.ToString();
   }
}