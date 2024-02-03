/*
 * Created by Chubo Zeko.
 * 
 * GitHub: https://github.com/chubozeko
 * LinkedIn: https://www.linkedin.com/in/chubo-zeko/
 * Game Catalog: https://chubozeko.itch.io/
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator animator;
    public Transform weaponHolder;
    public float speed = 5f;
    public bool isFacingRight = true;
    private float wHeight;
    private int health = 3;

    public float jumpSpeed = 7f;
    public bool isJumping = false;

    public AudioClip warriorHitSound;
    
    private float jumpButtonPressTime = 0f;
    private float maxJumpTime = 0.2f;

    private float rayCastLength = 0.005f;

    void Start()
    {
        health = 3;
    }

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        // wHeight: the bottom part of the Player
        wHeight = GetComponent<Collider2D>().bounds.extents.y + 0.1f;
    }


    void FixedUpdate()
    {
        /*** HORIZONTAL MOVEMENT ***/

        // Get Horizontal Movement (Walking)
        float horzMove = Input.GetAxisRaw("Horizontal");
        // Move Player according to direction
        Vector2 vect = rb.velocity;
        rb.velocity = new Vector2(horzMove * speed, vect.y);
        // Check movement direction and make player face towards direction
        FlipPlayer(horzMove);
        animator.SetFloat("HorizontalMove", Mathf.Abs(horzMove));

        /*** VERTICAL MOVEMENT ***/

        // Get Vertical Movement (Jumping)
        float vertMove = Input.GetAxisRaw("Vertical");
        if (IsOnGround() && isJumping == false)
        {
            if (vertMove > 0f)
            {
                isJumping = true;
            }
        }
        // Check if Player is busy jumping & jump time hasn't elapsed yet
        if (isJumping && jumpButtonPressTime < maxJumpTime)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
        // Check if Player is still busy jumping
        if (vertMove >= 1f)
        {
            jumpButtonPressTime += Time.deltaTime;
        }
        else
        {
            isJumping = false;
            jumpButtonPressTime = 0f;
        }
        // Check if Jumping time period has elapsed
        if (jumpButtonPressTime > maxJumpTime)
        {
            vertMove = 0f;
        }
        animator.SetBool("isJumping", isJumping);
    }

    private void FlipPlayer(float horzMove)
    {
        // Face RIGHT
        if (horzMove > 0 && !isFacingRight)
        {
            isFacingRight = !isFacingRight;
            sr.flipX = !sr.flipX;

            float direction = 0.5f;
            weaponHolder.position = new Vector2(
                transform.position.x + direction, 
                weaponHolder.position.y);
            weaponHolder.rotation = Quaternion.Euler(new Vector2(0f, 0f));
        }
        // Face LEFT
        if (horzMove < 0 && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            sr.flipX = !sr.flipX;

            float direction = -0.5f;
            weaponHolder.position = new Vector2(
                transform.position.x + direction,
                weaponHolder.position.y);
            weaponHolder.rotation = Quaternion.Euler(new Vector2(0f, 180f));
        }
    }

    public bool IsOnGround()
    {
        bool groundCheck = Physics2D.Raycast(
            new Vector2(transform.position.x, transform.position.y - wHeight),
            -Vector2.up,
            rayCastLength);

        if (groundCheck) return true;
        else return false;
    }

    public void GetHit(int damage)
    {
        health -= damage;
        FindObjectOfType<GameManager>().RemovePlayerLife(health);
        if (health <= 0)
        {
            Destroy(gameObject);
            Debug.Log("GAME OVER!!");
            FindObjectOfType<GameManager>().ShowGameOver(health);
            AudioManager.Instance.PlayGameSound(warriorHitSound);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Fire")
        {
            Debug.Log("LEVEL COMPLETE!!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Fire")
        {
            Debug.Log("LEVEL COMPLETE!!");
            FindObjectOfType<GameManager>().ShowLevelComplete(health);
        }

        if (collision.tag == "Respawn")
        {
            if (health < 5)
            {
                health++;
                FindObjectOfType<GameManager>().AddPlayerLife(health);
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnBecameInvisible()
    {
        Vector2 deathPosition = transform.position;
        Debug.Log("Position: " + deathPosition);
        GetHit(1);
        transform.position = new Vector2(deathPosition.x - 2f, deathPosition.y + 2f);
    }
}
