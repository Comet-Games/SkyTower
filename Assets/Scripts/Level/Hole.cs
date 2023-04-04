using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    public Transform respawnPlace;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            TopDownMovement player = other.GetComponent<TopDownMovement>();
            if(player.isDodging)
            {

            }
            else
            {
                player.TakeDamage(1);
                player.FallInPit(respawnPlace.position);
            }
            
        }
    }
}
