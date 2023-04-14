using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCircleShot : MonoBehaviour
{
    public Transform bulletSpawnPos;
    public float fireRate = 0.5f; // Time between shots
    public int magazineSize = 10; // Maximum number of bullets in a magazine
    public float reloadTime = 2f; // Time it takes to reload
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public int bulletsPerShot = 8; // Number of bullets in a circle
    public int maxMagazines = 5; // Maximum number of magazines
    public bool infiniteMagazines = false; // Whether the gun has an infinite number of magazines
    public int bulletsInMagazine; // Number of bullets currently in the magazine
    public float range;
    public int currentMagazines; // Number of magazines currently available

    private float nextFire = 0f;
    private bool reloading = false;

    // Start is called before the first frame update
    void Start()
    {
        currentMagazines = maxMagazines;
        bulletsInMagazine = magazineSize;
    }

    public void Fire(bool isPlayers)
    {
        if (bulletsInMagazine <= 0)
        {
            Reload();
        }
        else
        {
            if (Time.time > nextFire && bulletsInMagazine > 0 && !reloading)
            {
                nextFire = Time.time + fireRate;

                bulletsInMagazine--;

                int numBullets = bulletsPerShot;
                float angleStep = 360f / numBullets;

                for (int i = 0; i < numBullets; i++)
                {
                    GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPos.position, bulletSpawnPos.rotation);
                    bullet.GetComponent<SimpleBullet>().isPlayers = isPlayers;
                    float bulletAngle = i * angleStep;
                    bullet.transform.Rotate(0f, 0f, bulletAngle);
                    bullet.GetComponent<SimpleBullet>().speed = bulletSpeed;
                    bullet.GetComponent<SimpleBullet>().timeTillDestruction = range;
                    bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * bulletSpeed;
                }
            }
        }
    }

    private float reloadStartTime; // time when reloading starts

    public void Reload()
    {
        if (!infiniteMagazines)
        {
            if (!reloading && currentMagazines > 0 && bulletsInMagazine < magazineSize)
            {
                reloading = true;
                reloadStartTime = Time.time; // store the start time of reloading
                currentMagazines--;
                bulletsInMagazine = magazineSize;
                StartCoroutine(ReloadDelay());
            }
        }
        if (infiniteMagazines)
        {
            if (!reloading && bulletsInMagazine < magazineSize)
            {
                reloading = true;
                reloadStartTime = Time.time; // store the start time of reloading
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