using System.Collections;
using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    public float speed;
    public int damage;
    public float timeTillDestruction;
    public bool isPlayers = false;
    public Rigidbody2D rb;
    private GameObject player;
    private Collider2D coll;

    private void Start()
    {
        StartCoroutine(WaitTillDeath());
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        //rb.AddForce(transform.up * speed, ForceMode2D.Impulse);

        //transform.up = player.transform.position - transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (!isPlayers)
            {
                if(other.gameObject.GetComponent<TopDownMovement>().isDodging == false)
                {
                    other.gameObject.GetComponent<TopDownMovement>().TakeDamage(damage);
                    DestroySelf();
                }
            }       
        }
        if (other.gameObject.tag == "Enemy")
        {
            if(!isPlayers)
            {

            }
            else
            {
                other.gameObject.GetComponentInParent<EnemyHealth>().TakeDamage(damage);
                DestroySelf();
            }


        }
        if(other.gameObject.tag == "Blank")
        {
            DestroySelf();
        }

        if (other.gameObject.tag == "Wall")
        {
            DestroySelf();
        }   
    }

    private IEnumerator WaitTillDeath()
    {
        yield return new WaitForSeconds(timeTillDestruction);
        Destroy(gameObject);
    }

    private void DestroySelf()
    {
        // Animation and particles etc

        Destroy(gameObject);
    }
}
