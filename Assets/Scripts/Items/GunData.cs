using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Guns/GunData", fileName = "Gun Data")]
public class GunData : ScriptableObject
{
    public Sprite weaponSprite;
    public float fireRate = 0.5f; // Time between shots
    public int magazineSize = 10; // Maximum number of bullets in a magazine
    public float reloadTime = 2f; // Time it takes to reload
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float bulletSpread = 0.1f;
    public int bulletsPerShot = 1;
    public int maxMagazines = 5; // Maximum number of magazines
    public int damage = 1;
    public bool infiniteMagazines = false; // Whether the gun has an infinite number of magazines
    public int bulletsInMagazine; // Number of bullets currently in the magazine
    public float range;

    [Header("Sounds")]
    public AudioClip shoot;
    public AudioClip reload;
}
