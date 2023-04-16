using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoom : MonoBehaviour
{
    public GameObject EnemySpawner;
    public OpenDoors[] doors;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            foreach(OpenDoors door in doors)
            {
                door.CloseTheDoors();
                door.LockDoors();
            }
            EnemySpawner.SetActive(true);


            GetComponent<Collider2D>().enabled = false;
        }
    }

    public void OpenTheDoors()
    {
        foreach (OpenDoors door in doors)
        {
            door.CloseTheDoors();
            door.LockDoors();
        }
        foreach (OpenDoors door in doors)
        {            
            door.UnlockDoors();
            door.OpenTheDoors();
        }
        EnemySpawner.SetActive(false);
    }
}
