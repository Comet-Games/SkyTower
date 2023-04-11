using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGunScript : MonoBehaviour
{
    public Transform bulletSpawnPos;
    public float fireRate = 0.5f; // Time between shots
    public int magazineSize = 10; // Maximum number of bullets in a magazine
    public float reloadTime = 2f; // Time it takes to reload
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float bulletSpread = 0.1f;
    public int bulletsPerShot = 1;
    public int maxMagazines = 5; // Maximum number of magazines
    public bool infiniteMagazines = false; // Whether the gun has an infinite number of magazines
    public int bulletsInMagazine; // Number of bullets currently in the magazine
    public int currentMagazines; // Number of magazines currently available

    private float nextFire = 0f;
    private bool reloading = false;

    private void Start()
    {
        currentMagazines = maxMagazines;
        bulletsInMagazine = magazineSize;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (bulletsInMagazine <= 0)
            {
                Reload();
            }
            else
            {
                Fire();
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    public void Fire()
    {
        if (Time.time > nextFire && bulletsInMagazine > 0 && !reloading)
        {
            nextFire = Time.time + fireRate;

            bulletsInMagazine--;

            for (int i = 0; i < bulletsPerShot; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPos.position, bulletSpawnPos.rotation);
                bullet.GetComponent<SimpleBullet>().isPlayers = true;
                float totalSpreadAngle = bulletSpread * (bulletsPerShot - 1);
                float bulletAngle = (-totalSpreadAngle / 2f) + (i * bulletSpread);
                bullet.transform.Rotate(0f, 0f, bulletAngle);
                bullet.GetComponent<SimpleBullet>().speed = bulletSpeed;
                bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * bulletSpeed;
            }
        }
    }

    public void Reload()
    {
        if(!infiniteMagazines)
        {
            if (!reloading && currentMagazines > 0 && bulletsInMagazine < magazineSize)
            {
                reloading = true;
                currentMagazines--;
                bulletsInMagazine = magazineSize;
                StartCoroutine(ReloadDelay());
            }
        }
        if(infiniteMagazines)
        {
            if(!reloading && bulletsInMagazine < magazineSize)
            {
                reloading = true;
                currentMagazines = 1;
                bulletsInMagazine = magazineSize;
                StartCoroutine(ReloadDelay());
            }
        }

    }

    private IEnumerator ReloadDelay()
    {
        yield return new WaitForSeconds(reloadTime);
        reloading = false;
    }
}
