using UnityEngine;
using UnityEngine.AI;

public class SimpleFollowEnemy : MonoBehaviour
{
    public CircleCollider2D radiusCol;
    public float radius;
    private Transform target;
    NavMeshAgent agent;

    [Header("Weapon Stuff")]
    public GameObject weaponObj;
    public float AimSpeed;
    public bool holdingWeapon;
    bool shooting = true;
    public float bulletSpeed = 5;

    Animator animator;
    private void Start()
    {
        radiusCol.radius = radius;

        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = true;
        agent.updateRotation = false;
        animator = GetComponentInChildren<Animator>();
        animator.SetBool("Weaponless", !holdingWeapon);

        if(GetComponentInChildren<SimpleGunScript>())
        {
            holdingWeapon = true;
            weaponObj = GetComponentInChildren<SimpleGunScript>().gameObject;
            weaponObj.GetComponent<SimpleGunScript>().bulletSpeed = bulletSpeed;
        }
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
        }
    }

    void SetAgentPosition()
    {
        agent.SetDestination(new Vector3(target.position.x, target.position.y, transform.position.z));
        animator.SetBool("Moving", true);
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
