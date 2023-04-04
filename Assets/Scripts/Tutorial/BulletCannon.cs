using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCannon : MonoBehaviour
{
    public GameObject bullet;
    public float fireRate;
    public bool fire = false;

    float timer;

    void Update()
    {
        if(fire)
        {
            timer += Time.deltaTime;
            if (timer > fireRate)
            {
                GameObject newBullet = Instantiate(bullet, transform);
                timer = 0;
            }
        }

    }

}
