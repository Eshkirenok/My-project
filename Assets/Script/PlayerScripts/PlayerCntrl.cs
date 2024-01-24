using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCntrl : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 2f;
    public Vector2 moveVector;
    public bool faceRight = true;
    public float jumpForce;
    private int jumpCount = 0;
    public int maxJumpValue;
    public bool onGround;
    public LayerMask Ground;
    public Transform GroundCheck;
    private float CheckRadius = 0.8f;

    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Walk();
        Jump();
        CheckingGround();
    }

    private void Walk()
    {
        moveVector.x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y);
        Reflect();

        // Управление анимацией бега и покоя
        if (moveVector.x != 0)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
    }

    private void Reflect()
    {
        if ((moveVector.x > 0 && !faceRight) || (moveVector.x < 0 && faceRight))
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            faceRight = !faceRight;
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onGround)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            else if (++jumpCount < maxJumpValue)
            {
                rb.velocity = new Vector2(0, 28);
            }
        }

        if (onGround)
        {
            jumpCount = 0;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            Physics2D.IgnoreLayerCollision(7, 8, true);
            Invoke("IgnoreLayerOff", 0.15f);
        }
    }

    private void IgnoreLayerOff()
    {
        Physics2D.IgnoreLayerCollision(7, 8, false);
    }

   private void CheckingGround()
{
    onGround = Physics2D.OverlapCircle(GroundCheck.position, CheckRadius, Ground);

    // Управление анимацией прыжка и падения
    animator.SetBool("IsJumping", !onGround && rb.velocity.y > 0);
    animator.SetBool("IsFalling", !onGround && rb.velocity.y < 0);
}
}