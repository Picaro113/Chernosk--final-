using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;

    public void Start()
    {
        target = GameObject.Find("Player").transform;
        if (target == null )
        {
            Debug.Log("Target has not been found");
        }
        agent = GetComponent<NavMeshAgent>();
    }

    public void Update()
    {

    }
}
