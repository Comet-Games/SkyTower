using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class VendingMachine : MonoBehaviour
{
    private bool inRange;
    private GameObject player;
    public GameObject pickup;
    public Transform spawn;
    public Sprite emptySprite;
    public Material outlineMat;

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
                GetComponentInChildren<SpriteRenderer>().sprite = emptySprite;
                GetComponentInChildren<Light2D>().gameObject.SetActive(false);
                Instantiate(pickup, spawn);
            }
        }
    }

    private void Awake()
    {
        outlineMat = GetComponentInChildren<SpriteRenderer>().material;
        outlineMat.SetFloat("_OutlineThickness", 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && used == false)
        {
            outlineMat.SetFloat("_OutlineThickness", 2);
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
            outlineMat.SetFloat("_OutlineThickness", 0);
            inRange = false;
            Debug.Log("Player out of range");
            player.GetComponent<TopDownMovement>().DeactivateInteractableText();
        }
    }
}
