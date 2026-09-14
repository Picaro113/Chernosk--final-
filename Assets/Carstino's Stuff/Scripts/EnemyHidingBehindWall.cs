using UnityEngine;
using UnityEngine.AI;

public class EnemyHidingBehindWall : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform bleh;

    public bool hasCompared;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        hasCompared = false;
    }

    private void Update()
    {
        if (hasCompared == true)
        {
            Debug.Log(hasCompared + " hasCompared");
            agent.SetDestination(transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            transform.position = other.gameObject.transform.position;
            hasCompared = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            hasCompared = false;
        }
    }
}
