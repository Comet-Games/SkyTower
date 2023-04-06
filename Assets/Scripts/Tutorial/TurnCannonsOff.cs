using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCannonsOff : MonoBehaviour
{
    public BulletCannon[] cannons;
    public bool switc;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            foreach(BulletCannon b in cannons)
            {
                b.fire = switc;
            }
        }

    }
}
