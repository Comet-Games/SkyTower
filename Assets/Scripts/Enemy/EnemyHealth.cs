using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.SceneManagement;
using Edgar.Unity.Examples.Gungeon;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    [HideInInspector] public int health = 5;
    [HideInInspector] public int shield = 1;
    [HideInInspector] public bool canTakeDamage;
    [HideInInspector] public float healthTimer;
    private float Htimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canTakeDamage == false)
        {
            Htimer += Time.deltaTime;
            if (Htimer > healthTimer)
            {
                Htimer = 0;
                canTakeDamage = true;
            }
        }
    }

    public void TakeDamage(int amount)
    {

        if (canTakeDamage)
        {
            if (shield > 0)
            {
                shield--;
                Debug.Log("Enemy Shield Just Got Hit, Shield is now: " + shield);
                if (shield <= 0)
                {
                    Debug.Log("Enemy Shield Just Broke");
                    shield = 0;
                }
            }
            else
            {
                health = health - amount;
                Debug.Log("Enemy Just Got Hit, Health is now: " + health);
                if (health <= 0)
                {
                    Debug.Log("The Enemy Just Died");
                    GetComponent<GungeonEnemy>().OnKilled();
                    //Die();
                }
            }
            canTakeDamage = false;
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
