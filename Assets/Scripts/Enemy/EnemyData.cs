using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/EnemyData", fileName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("General Stuff")]
    public string enemyName;
    public Rarity rarity;
    public enum Rarity { common, uncommon, rare, epic, legendary, mythic };
    public Animator enemyAnimation;
    public float moveSpeed = 4;

    [Header("Health Stuff")]
    public int health = 5;
    public int shield = 0;
    public float healthTimer;

    [Header("Weapon Stuff")]
    public Sprite weaponSprite;
    public float AimSpeed = 100;
    public bool holdingWeapon = true;
    public float fireRate = 0.5f; // Time between shots
    public int magazineSize = 10; // Maximum number of bullets in a magazine
    public float reloadTime = 2f; // Time it takes to reload
    public float bulletSpeed = 5; // Speed of the bullet
    public float bulletSpread = 0.1f; // Spread Of the bullet(s)
    public int bulletsPerShot = 1; // Amount of bullets Per Shot
    public int maxMagazines = 5; // Maximum number of magazines
    public bool infiniteMagazines = false; // Whether the gun has an infinite number of magazines

    public float radius = 50; // Range the enemy can "see" the player
}
