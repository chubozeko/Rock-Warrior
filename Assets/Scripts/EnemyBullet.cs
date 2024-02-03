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

public class EnemyBullet : MonoBehaviour
{
    public float speed = 15f;
    public Rigidbody2D rb;
    public int bulletDamage = 1;
    void Start()
    {
        // Move forward when initialized
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Player warrior = col.GetComponent<Player>();
            if (warrior != null)
            {
                warrior.GetHit(bulletDamage);
                FindObjectOfType<GameManager>().RemovePoints(20);
                Destroy(gameObject);
                // TODO: Add Sound Effect
            }

            // TODO: Add Impact Animation
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
