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

public class Weapon : MonoBehaviour
{
    public Transform firepoint;
    public GameObject bulletPrefab;

    public float damage = 40f;
    public float range = 2f;
    public float fireRate = 5f;
    public bool hasInfiniteAmmo = false;
    public int maxAmmo = 5;
    public int totalAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;
    private bool isAmmoFinished = false;
    //private float nextTimeToFire = 0f;
    public AudioClip weaponShotSound;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    private void Awake()
    {
        currentAmmo = maxAmmo;
    }
    private void OnEnable()
    {
        isReloading = false;
    }

    void Update()
    {
        if(!hasInfiniteAmmo)
        {
            // If busy reloading weapon, skip and wait
            if (isReloading)
            {
                return;
            }
            // If current Ammo storage is empty and there is more Ammo available,
            // Reload weapon
            if (currentAmmo <= 0 && totalAmmo > 0)
            {
                StartCoroutine(Reload());
                return;
            }
            // If Ammo is Finished
            if (totalAmmo <= 0)
            {
                isAmmoFinished = true;
            }
        }
        
        // Shoot Bullet
        if (Input.GetButtonDown("Fire1") && gameObject.GetComponent<Player>() != null)
        {
            Shoot();
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        if (maxAmmo > totalAmmo)
            currentAmmo = totalAmmo;
        else
            currentAmmo = maxAmmo;
        isReloading = false;
    }

    public void Shoot()
    {
        if (!isAmmoFinished)
        {
            // Disable Shooter Collider to prevent collision detection
            if (gameObject.tag == "Player")
                FindObjectOfType<Player>().GetComponent<BoxCollider2D>().enabled = false;
            // Prefab Shooting Logic
            Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
            currentAmmo--;
            totalAmmo--;
            // Enable Shooter Collider
            if (gameObject.tag == "Player")
                FindObjectOfType<Player>().GetComponent<BoxCollider2D>().enabled = true;
                
            AudioManager.Instance.PlayGameSound(weaponShotSound);
        }
    }
}
