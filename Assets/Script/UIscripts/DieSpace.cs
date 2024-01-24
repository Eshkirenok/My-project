using UnityEngine;

public class DieSpace : MonoBehaviour 
{
public GameObject resp;
public float damage = 40f;
void OnTriggerEnter2D (Collider2D other) {
    if (other.tag == "Player")
    other.transform.position = resp.transform.position;
    other.GetComponent <PlayerInteraction> ().TakeDamage(damage);
}
}