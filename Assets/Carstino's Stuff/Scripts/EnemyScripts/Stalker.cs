using UnityEngine;
using UnityEngine.AI;

public class Stalker : MonoBehaviour
{
    public EnemyDetection detector;
    private NavMeshAgent agent;
    public GameObject Player;
    private Vector3 point;

    public void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
            if (agent == null)
            {
                Debug.LogWarning("No agent found");
            }
        }
    }

    void Update()
    {

        if (detector.infront == true)
        {
            agent.SetDestination(Player.transform.position);
        }
        if (detector.infront == false)
        {
            agent.SetDestination(transform.position);
        }
    }
}
