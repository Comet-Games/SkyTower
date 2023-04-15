using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BossStateController : MonoBehaviour
{
    [Header("References")]
    public CircleCollider2D radiusCol;
    public EnemyHealth enemyHealth;
    public Transform target;
    NavMeshAgent agent;

    [Header("Health")]
    public int health = 5;
    public int shield = 1;
    public float healthTimer;
    public SimpleGunScript gun;
    public GameObject gunObj;
    public SimpleCircleShot circleGun;
    private float maxHealth;

    [Header("HealthBar")]
    public HealthBar healthBar;
    public string name;

    [Header("Stages")]
    public int stage1Health;
    public Color stage1Color;
    public int stage2Health;
    public Color stage2Color;
    public int stage3Health;
    public Color stage3Color;

    public enum BossState
    {
        Stage1,
        Stage2,
        Stage3,
        Dead
    }

    public BossState currentState;

    private void Start()
    {
        maxHealth = health;
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = true;
        agent.updateRotation = false;
        currentState = BossState.Stage2;
        enemyHealth = GetComponent<EnemyHealth>();
        enemyHealth.health = health;
        enemyHealth.shield = shield;
        enemyHealth.healthTimer = healthTimer;
        gunObj = gun.gameObject;
        healthBar.SetName(name);
    }

    private void Update()
    {
        float healthPercentage = ((float)enemyHealth.health / maxHealth);
        healthBar.SetSize(healthPercentage);
        if (enemyHealth.health >= 0)
        {
            if (enemyHealth.health >= stage1Health)
            {
                currentState = BossState.Stage1;
            }
            else if(enemyHealth.health >= stage2Health && enemyHealth.health < stage1Health)
            {
                currentState = BossState.Stage2;
            }
            else if(enemyHealth.health >= stage3Health && enemyHealth.health < stage2Health)
            {
                currentState = BossState.Stage3;
            }
        }
        else
        {
            currentState = BossState.Dead;
            Destroy(gameObject);
        }

        switch (currentState)
        {
            case BossState.Stage1:
                healthBar.SetColour(stage1Color);
                gunObj.GetComponent<GunLookAtMouse>().EnemyVersion(target.position);
                agent.SetDestination(new Vector3(target.position.x, target.position.y, transform.position.z));
                gun.Fire(false);
                break;

            case BossState.Stage2:
                // Code for stage 2 state
                healthBar.SetColour(stage2Color);
                Vector3 rotationToAdd = new Vector3(0, 0, 0.5f);
                gunObj.GetComponent<GunLookAtMouse>().EnemyVersion(target.position);
                circleGun.transform.Rotate(rotationToAdd);
                circleGun.Fire(false);
                break;

            case BossState.Stage3:
                healthBar.SetColour(stage3Color);
                gunObj.GetComponent<GunLookAtMouse>().EnemyVersion(target.position);
                agent.SetDestination(new Vector3(target.position.x, target.position.y, transform.position.z));
                circleGun.Fire(false);
                gun.Fire(false);
                break;

            case BossState.Dead:
                Debug.Log("Boss is dead");
                break;

            default:
                // Default code if none of the states match
                break;
        }
    }

    public void ChangeState(BossState newState)
    {
        currentState = newState;
    }
}