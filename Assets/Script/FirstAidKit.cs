using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAidKit : MonoBehaviour
{
public float damage = -10f;
public GameObject interactButton;


private void Start()
{
    interactButton = GameObject.Find("InteractButton");
    interactButton.SetActive(false);
}

private void OnTriggerEnter2D (Collider2D other)
{
    if (other.name == "Rogue" || other.name == "Wizard" || other.name == "Knight")
    {
        interactButton.SetActive(true);
    }
}

private void OnTriggerExit2D (Collider2D other)
{
    if (other.name == "Rogue" || other.name == "Wizard" || other.name == "Knight")
    {
        interactButton.SetActive(false);
    }
}

public void Interact()
{
    interactButton.SetActive(false);
    PlayerInteraction playerInteraction = GameObject.FindWithTag("Player").GetComponent<PlayerInteraction>();
    playerInteraction.TakeDamage(damage);
    gameObject.SetActive(false);
}

}