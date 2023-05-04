using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : MonoBehaviour
{
    private bool inRange;
    private GameObject player;
    public GameObject pickup;
    public Transform spawn;

    private bool used = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && player != null && used == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                used = true;
                Instantiate(pickup, spawn);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && used == false)
        {
            player = other.gameObject;
            inRange = true;
            Debug.Log("Player in range");
            player.GetComponent<TopDownMovement>().ActivateInteractableText();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inRange = false;
            Debug.Log("Player out of range");
            player.GetComponent<TopDownMovement>().DeactivateInteractableText();
        }
    }
}
