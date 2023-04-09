using UnityEngine;
using UnityEngine.AI;
public class SimpleFollowEnemy : MonoBehaviour
{
    public CircleCollider2D radiusCol;
    public float radius;
    private Transform target;
    NavMeshAgent agent;
    private void Start()
    {
        radiusCol.radius = radius;

        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = true;
        agent.updateRotation = false;
    }
    private void Update()
    {
        if(target != null)
        {
            SetAgentPosition();
        }

    }

    void SetAgentPosition()
    {
        agent.SetDestination(new Vector3(target.position.x, target.position.y, transform.position.z));
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

}
