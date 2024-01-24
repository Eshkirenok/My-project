using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public float damage = 10f;
    public void OnTriggerEnter2D (Collider2D other)
   {
        if (other.name == "Rogue")
        {
            Debug.Log("Шип разбойницу");
            other.GetComponent <PlayerInteraction> ().TakeDamage(damage);
        }
        if (other.name == "Wizard")
        {
            Debug.Log("Шип мага");
            other.GetComponent <PlayerInteraction> ().TakeDamage(damage);
        }
        if (other.name == "Knight")
        {
            Debug.Log("Шип рыцаря");
            other.GetComponent <PlayerInteraction> ().TakeDamage(damage);
        }
   }
}
