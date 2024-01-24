using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
     public float speed = 5f;
    public float jumpForce = 10f;
    public float followRange = 5f;
    private Transform target;
    private bool targetFound = false;
    private Rigidbody2D rb;
    private bool onGround = true;

    void Start()
    {
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (target.gameObject.activeInHierarchy && !targetFound)
        {
            targetFound = true;
        }

        if (targetFound)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance < followRange)
            {
                float xDirection = Mathf.Sign(target.position.x - transform.position.x);
                transform.position = transform.position + new Vector3(xDirection * speed * Time.deltaTime, 0, 0);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            onGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            onGround = false;
        }
    }

    private void FixedUpdate()
    {
        if (onGround && Mathf.Abs(target.position.x - transform.position.x) < followRange)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            onGround = false;
        }
    }
}
