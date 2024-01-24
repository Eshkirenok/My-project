using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInteraction : MonoBehaviour
{
   public float health = 100f;
   public float damage = 25f;
   public float attackInterval = 1.5f;
   public Image LineBar;
   private Collider2D playerCollider;

   public void OnTriggerEnter2D (Collider2D other)
   {
        if (other.name == "Rogue" || other.name == "Wizard" || other.name == "Knight")
        {
            playerCollider = other;
            StartCoroutine(Attack());
        }
   }

   public void OnTriggerExit2D(Collider2D other)
   {
    if (other.name == "Rogue" || other.name == "Wizard" || other.name == "Knight")
    {
        playerCollider = null;
        StopCoroutine(Attack());
    }
   }
   IEnumerator Attack()
   {
        while (playerCollider != null)
        {
            playerCollider.GetComponent <PlayerInteraction>().TakeDamage(damage);
            yield return new WaitForSeconds(attackInterval);
        }
   }
   public void TakeDamage (float amount)
   {
    health -= amount;
    LineBar.fillAmount = health/100;
    if(health<= 0f)
    {
        Die();
    }
   }
   void Die()
   {
    StopCoroutine(Attack());
    Destroy(gameObject);
   }
}