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

public class Cattle : MonoBehaviour
{
    public float health = 100f;
    public float shootingTime = 20f;
    public Weapon wHolder;
    public AudioClip cattleHitSound;
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
        FindObjectOfType<GameManager>().AddPoints(30);
        if(health <= 0f)
        {
            Destroy(gameObject);
            FindObjectOfType<GameManager>().AddPoints(50);
            AudioManager.Instance.PlayGameSound(cattleHitSound);
        }
    }

    IEnumerator WaitForNextShot()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootingTime);
        canShoot = true;
    }
}
