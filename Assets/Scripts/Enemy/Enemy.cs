using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;
    CircleCollider2D radiusCol;
    EnemyHealth enemyHealth;
    Transform target;
    SimpleGunScript gunScript;
    NavMeshAgent agent;
    Animator animator;
    bool shooting;

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        radiusCol = GetComponent<CircleCollider2D>();
        radiusCol.radius = enemyData.radius;
        agent.updatePosition = true;
        agent.updateRotation = false;
        animator.SetBool("Weaponless", !enemyData.holdingWeapon);
        enemyHealth.health = enemyData.health;
        enemyHealth.shield = enemyData.shield;
        enemyHealth.healthTimer = enemyData.healthTimer;
        if (enemyData.holdingWeapon == true)
        {
            gunScript = GetComponentInChildren<SimpleGunScript>();
            enemyData.holdingWeapon = true;
            gunScript.fireRate = enemyData.fireRate;
            gunScript.magazineSize = enemyData.magazineSize;
            gunScript.reloadTime = enemyData.reloadTime;
            gunScript.bulletSpeed = enemyData.bulletSpeed;
            gunScript.bulletSpread = enemyData.bulletSpread;
            gunScript.bulletsPerShot = enemyData.bulletsPerShot;
            gunScript.maxMagazines = enemyData.maxMagazines;
            gunScript.infiniteMagazines = enemyData.infiniteMagazines;
        }
    }

    private void Update()
    {
        if (target != null)
        {
            SetAgentPosition();

            Vector3 dir = transform.position - target.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            dir.Normalize();

            if (enemyData.holdingWeapon)
            {
                gunScript.gameObject.GetComponent<GunLookAtMouse>().EnemyVersion(target.position);
                if (shooting)
                {

                    gunScript.gameObject.GetComponent<SimpleGunScript>().Fire(false);
                }
            }

            animator.SetFloat("MouseHorizontal", -dir.x);
            animator.SetFloat("MouseVertical", -dir.y);
            animator.SetFloat("Horizontal", -dir.x);
            animator.SetFloat("Vertical", -dir.y);
        }

        if (agent.velocity.magnitude <= 1)
        {
            animator.SetBool("Moving", false);
            shooting = true;
        }
        else
        {
            shooting = false;
        }
    }

    void SetAgentPosition()
    {
        agent.SetDestination(new Vector3(target.position.x, target.position.y, transform.position.z));
        animator.SetBool("Moving", true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = null;
        }
    }
}
