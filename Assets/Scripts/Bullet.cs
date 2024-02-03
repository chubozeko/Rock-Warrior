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

public class Bullet : MonoBehaviour
{
    public float speed = 15f;
    public Rigidbody2D rb;
    public float bulletDamage;
    void Start()
    {
        // Move forward when initialized
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Cattle")
        {
            Cattle cattle = col.GetComponent<Cattle>();
            if (cattle != null)
            {
                cattle.GetHit(bulletDamage);
                Destroy(gameObject);
                // TODO: Add Sound Effect
            }

            // TODO: Add Impact Animation
        }
        if (col.tag == "Ox")
        {
            Ox ox = col.GetComponent<Ox>();
            if (ox != null)
            {
                ox.GetHit(bulletDamage);
                Destroy(gameObject);
                // TODO: Add Sound Effect
            }

            // TODO: Add Impact Animation
        }
        if (col.tag == "Mammoth")
        {
            Mammoth mammoth = col.GetComponent<Mammoth>();
            if (mammoth != null)
            {
                mammoth.GetHit(bulletDamage);
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
