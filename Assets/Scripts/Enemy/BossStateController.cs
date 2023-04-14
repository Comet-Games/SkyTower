using UnityEngine;
using UnityEngine.AI;

public class BossStateController : MonoBehaviour
{
    [Header("References")]
    public CircleCollider2D radiusCol;
    public EnemyHealth enemyHealth;

    [Header("Health")]
    public int health = 5;
    public int shield = 1;
    public float healthTimer;
    public SimpleGunScript gun;
    public SimpleCircleShot circleGun;

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
        currentState = BossState.Stage2;
        enemyHealth = GetComponent<EnemyHealth>();
        enemyHealth.health = health;
        enemyHealth.shield = shield;
        enemyHealth.healthTimer = healthTimer;
    }

    private void Update()
    {
        switch (currentState)
        {
            case BossState.Stage1:
                gun.Fire(false);
                break;

            case BossState.Stage2:
                // Code for stage 2 state
                Vector3 rotationToAdd = new Vector3(0, 0, 0.5f);
                transform.Rotate(rotationToAdd);
                circleGun.Fire(false);
                break;

            case BossState.Stage3:
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