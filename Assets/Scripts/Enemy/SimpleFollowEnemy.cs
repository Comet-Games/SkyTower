using UnityEngine;
using UnityEngine.AI;

public class SimpleFollowEnemy : MonoBehaviour
{
    [Header("References")]
    public CircleCollider2D radiusCol;
    public EnemyHealth enemyHealth;
    private Transform target;
    NavMeshAgent agent;

    [Header("Health")]
    public int health = 5;
    public int shield = 1;
    public float healthTimer;

    [Header("Weapon Stuff")]
    public GameObject weaponObj;
    public float AimSpeed;
    public bool holdingWeapon;
    bool shooting = true;
    public float fireRate = 0.5f; // Time between shots
    public int magazineSize = 10; // Maximum number of bullets in a magazine
    public float reloadTime = 2f; // Time it takes to reload
    public float bulletSpeed = 5; // Speed of the bullet
    public float bulletSpread = 0.1f; // Spread Of the bullet(s)
    public int bulletsPerShot = 1; // Amount of bullets Per Shot
    public int maxMagazines = 5; // Maximum number of magazines
    public bool infiniteMagazines = false; // Whether the gun has an infinite number of magazines

    public float radius;
    Animator animator;
    private void Start()
    {
        GetReferences();
        SetReferences();
        radiusCol.radius = radius;

        agent.updatePosition = true;
        agent.updateRotation = false;
        animator.SetBool("Weaponless", !holdingWeapon);
    }
    private void Update()
    {
        if(target != null)
        {
            SetAgentPosition();

            Vector3 dir = transform.position - target.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            dir.Normalize();

            if(holdingWeapon)
            {
                if(shooting)
                {
                    weaponObj.GetComponent<GunLookAtMouse>().EnemyVersion(target.position);
                    weaponObj.GetComponent<SimpleGunScript>().Fire(false);
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

    void GetReferences()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        if (holdingWeapon == true)
        {
            holdingWeapon = true;
            weaponObj = GetComponentInChildren<SimpleGunScript>().gameObject;
            SimpleGunScript weaponScript = GetComponentInChildren<SimpleGunScript>();
            weaponScript.fireRate = fireRate;
            weaponScript.magazineSize = magazineSize;
            weaponScript.reloadTime = reloadTime;
            weaponScript.bulletSpeed = bulletSpeed;
            weaponScript.bulletSpread = bulletSpread;
            weaponScript.bulletsPerShot = bulletsPerShot;
            weaponScript.maxMagazines = maxMagazines;
            weaponScript.infiniteMagazines = infiniteMagazines;
        }
    }

    void SetReferences()
    {
        enemyHealth.health = health;
        enemyHealth.shield = shield;
        enemyHealth.healthTimer = healthTimer;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
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
