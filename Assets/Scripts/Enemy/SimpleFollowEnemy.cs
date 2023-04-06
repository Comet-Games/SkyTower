using UnityEngine;

public class SimpleFollowEnemy : MonoBehaviour
{
    public float speed = 3f;
    public CircleCollider2D radiusCol;
    public float radius;
    private Transform target;
    private Rigidbody2D body;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        radiusCol.radius = radius;
    }

    private void Update()
    {
        if(target != null)
        {
            float step = speed * Time.deltaTime;
            Vector2 playerPos = Vector2.MoveTowards(transform.position, target.position, step);
            body.MovePosition(playerPos);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            target = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            TopDownMovement playerScript = other.gameObject.GetComponent<TopDownMovement>();
            playerScript.TakeDamage(1);
        }
    }

}
