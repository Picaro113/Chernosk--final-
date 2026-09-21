using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Searchers : MonoBehaviour
{
    public EnemyObjects enemyObjects;

    public MainEnemyState stateMachine;

    public NavMeshAgent agent;
    public Transform planeVector;
    public float hunger = 50;
    public Vector3 point;

    public float timeBeforeMoving;
    public float timeToMove = 3;

    public List<TestFood> foods = new List<TestFood>();
    public List<Searchers> searchers = new List<Searchers>();

    public bool callFunctionClosestObject;

    public float RadiusOfSphere;
    public float angle = 90f;

    private void Start()
    {
        //OtherComponents
        agent = GetComponent<NavMeshAgent>();
        TestFood[] food = GameObject.FindObjectsByType<TestFood>(FindObjectsSortMode.None);
        Searchers[] searcher = GameObject.FindObjectsByType<Searchers>(FindObjectsSortMode.None);
        searchers.AddRange(searcher);
        foods.AddRange(food);
        callFunctionClosestObject = false;
        Debug.Log(enemyObjects.myString);

        //StateMachine Initialization
        stateMachine = new MainEnemyState(this);
        stateMachine.Initialize(stateMachine.searchState);
    }

    private void Update()
    {
        Collider[] EnemiesInRadius = Physics.OverlapSphere(transform.position, RadiusOfSphere);

        foreach(Collider Enemy in EnemiesInRadius)
        {
            if (Enemy.gameObject.TryGetComponent<Searchers>(out Searchers searchers) != this.gameObject)
            {
                Debug.Log("We've got more enemies");
            }
            else
            {
                Debug.Log("It's something else");
            }
        }

        //Transform target = EnemiesInRadius[0].transform;
        //Vector3 directionToTarget = (target.position - transform.position).normalized;
        //
        //if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
        //{
        //    float distanceToTarget = Vector3.Distance(transform.position, target.position);
        //
        //    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget))
        //    {
        //        Debug.Log("see player");
        //    }
        //    else
        //    {
        //        Debug.Log("Can't see player");
        //    }
        //}

        hunger -= Time.deltaTime;
        stateMachine.Update();
    }

    public bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            randomPoint.y = center.y;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, agent.areaMask))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
    public TestFood closestObject()
    {
        TestFood closestTarget = null;
        float closestDistanceSqr = float.MaxValue;

        foreach (TestFood food in foods)
        {
            if (food == null) continue;

            Vector3 directionToTarget = food.transform.position - transform.position;
            float dSqrToTarget = directionToTarget.sqrMagnitude;

            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                closestTarget = food;
            }
        }
        if (closestTarget != null)
        {
            agent.SetDestination(closestTarget.transform.position);
        }
        callFunctionClosestObject = true;
        return closestTarget;
    }
}
