using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    public float health = 100f;
    public Image LineBar;
    public Transform attackPoint;
    public float AttackRange = 0.5f;
    public LayerMask enemyLayers;
    public float playerdamage = 25f;
    public float attackDelay = 1f;
    private float nextAttackTime;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && Time.time >= nextAttackTime)
        {
            Attack();
        }
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, AttackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyInteraction>().TakeDamage(playerdamage);
        }

        nextAttackTime = Time.time + attackDelay;

        if (Input.GetKeyDown(KeyCode.J) && Time.time >= nextAttackTime)
        {
            PlayAttackAnimation2();
        }
        else
        {
            PlayAttackAnimation1();
        }
    }

    void PlayAttackAnimation1()
    {
        animator.SetTrigger("Attack1");
    }

    void PlayAttackAnimation2()
    {
        animator.SetTrigger("Attack2");
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, AttackRange);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        LineBar.fillAmount = health / 100;
        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        SceneManager.LoadScene(7);
    }
}