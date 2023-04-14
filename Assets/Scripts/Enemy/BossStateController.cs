using UnityEngine;

public class BossStateController : MonoBehaviour
{
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
    }

    private void Update()
    {
        switch (currentState)
        {
            case BossState.Stage1:
                // Code for stage 1 state
                gun.Fire(false);
                break;

            case BossState.Stage2:
                // Code for stage 2 state
                circleGun.Fire(false);
                break;

            case BossState.Stage3:
                // Code for stage 3 state
                circleGun.Fire(false);
                gun.Fire(false);
                break;

            case BossState.Dead:
                // Code for dead state
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