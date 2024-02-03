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

public class Ox : MonoBehaviour
{
    public float health = 150f;
    public float shootingTime = 50f;
    public Weapon wHolder;
    public AudioClip oxHitSound;
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
            Destroy(gameObject);
            AudioManager.Instance.PlayGameSound(oxHitSound);
        }
    }

    IEnumerator WaitForNextShot()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootingTime);
        canShoot = true;
    }
}
