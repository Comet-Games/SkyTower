using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public GunData gunData;
    public Transform bulletSpawnPos;
    private int bulletsInMagazine; // Number of bullets currently in the magazine
    private int currentMagazines; // Number of magazines currently available*/

    private float nextFire = 0f;
    public bool reloading = false;

    public AudioSource audioSource;

    public Slider reloadSlider;

    private void Start()
    {
        currentMagazines = gunData.maxMagazines;
        bulletsInMagazine = gunData.magazineSize;
        audioSource = GetComponent<AudioSource>();

        reloadSlider.minValue = 0f;
        reloadSlider.maxValue = gunData.reloadTime;
        reloadSlider.value = gunData.reloadTime; // start at the right
        reloadSlider.gameObject.SetActive(false);

    }

    private void Awake()
    {
        GetComponentInParent<TopDownMovement>().holdingWeapon = true;
    }

    private void Update()
    {

        if (Input.GetMouseButton(0))
        {
            Fire(true);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        // Update the slider value if reloading
        if (reloading)
        {
            reloadSlider.value = gunData.reloadTime - (Time.time - reloadStartTime);
        }
    }

    public void Fire(bool isPlayers)
    {
        if (bulletsInMagazine <= 0)
        {
            Reload();
            GetComponentInParent<TopDownMovement>().UpdateBullets(0);
        }
        else
        {
            if (Time.time > nextFire && bulletsInMagazine > 0 && !reloading)
            {
                nextFire = Time.time + gunData.fireRate;

                bulletsInMagazine--;
                GetComponentInParent<TopDownMovement>().UpdateBullets(bulletsInMagazine);

                if (gunData.bulletsPerShot == 1)
                {
                    GameObject bullet = Instantiate(gunData.bulletPrefab, bulletSpawnPos.position, bulletSpawnPos.rotation);
                    bullet.GetComponent<SimpleBullet>().isPlayers = isPlayers;
                    float bulletAngle = Random.Range(-gunData.bulletSpread, gunData.bulletSpread);
                    bullet.transform.Rotate(0f, 0f, bulletAngle);
                    bullet.GetComponent<SimpleBullet>().speed = gunData.bulletSpeed;
                    bullet.GetComponent<SimpleBullet>().timeTillDestruction = gunData.range;
                    bullet.GetComponent<SimpleBullet>().damage = gunData.damage;
                    bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * gunData.bulletSpeed;
                }
                else
                {
                    for (int i = 0; i < gunData.bulletsPerShot; i++)
                    {
                        GameObject bullet = Instantiate(gunData.bulletPrefab, bulletSpawnPos.position, bulletSpawnPos.rotation);
                        bullet.GetComponent<SimpleBullet>().isPlayers = isPlayers;
                        float totalSpreadAngle = gunData.bulletSpread * (gunData.bulletsPerShot - 1);
                        float bulletAngle = (-totalSpreadAngle / 2f) + (i * gunData.bulletSpread);
                        bullet.transform.Rotate(0f, 0f, bulletAngle);
                        bullet.GetComponent<SimpleBullet>().speed = gunData.bulletSpeed;
                        bullet.GetComponent<SimpleBullet>().damage = gunData.damage;
                        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * gunData.bulletSpeed;
                    }
                }
                audioSource.PlayOneShot(gunData.shoot, 0.7F);
            }
        }

    }

    private float reloadStartTime; // time when reloading starts

    public void Reload()
    {
        if (!gunData.infiniteMagazines)
        {
            if (!reloading && currentMagazines > 0 && bulletsInMagazine < gunData.magazineSize)
            {
                reloading = true;
                reloadSlider.gameObject.SetActive(true);
                reloadStartTime = Time.time; // store the start time of reloading
                audioSource.PlayOneShot(gunData.reload, 0.7f);
                currentMagazines--;
                bulletsInMagazine = gunData.magazineSize;
                StartCoroutine(ReloadDelay());
            }
        }
        if (gunData.infiniteMagazines)
        {
            if (!reloading && bulletsInMagazine < gunData.magazineSize)
            {
                reloading = true;
                reloadSlider.gameObject.SetActive(true);
                reloadStartTime = Time.time; // store the start time of reloading
                audioSource.PlayOneShot(gunData.reload, 0.7f);
                currentMagazines = 1;
                bulletsInMagazine = gunData.magazineSize;
                StartCoroutine(ReloadDelay());
            }
        }

    }

    private IEnumerator ReloadDelay()
    {
        GetComponentInParent<TopDownMovement>().UpdateBullets(0);
        yield return new WaitForSeconds(gunData.reloadTime);
        reloading = false;
        reloadSlider.gameObject.SetActive(false);
    }
}
