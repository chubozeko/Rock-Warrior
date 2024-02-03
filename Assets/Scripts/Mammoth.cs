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

public class Mammoth : MonoBehaviour
{
    public float health = 200f;
    public float shootingTime = 50f;
    public Weapon wHolder;
    public Sprite mammothDeath;
    public AudioClip mammothHitSound;
    private bool canShoot = true;
    void Start()
    {
        wHolder = GetComponent<Weapon>();
    }

    void Update()
    {
        if (!FindObjectOfType<GameManager>().isGamePaused)
        {
            if (canShoot)
            {
                wHolder.Shoot();
                StartCoroutine(WaitForNextShot());
            }
        }
    }

    public void GetHit(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            // Destroy(gameObject);
            AudioManager.Instance.PlayGameSound(mammothHitSound);
            StartCoroutine(DecayMammothBody());
        }
    }

    IEnumerator WaitForNextShot()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootingTime);
        canShoot = true;
    }
    
    IEnumerator DecayMammothBody()
    {
        gameObject.GetComponent<Animator>().SetBool("isDead", true);
        gameObject.GetComponent<SpriteRenderer>().sprite = mammothDeath;
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }
}
