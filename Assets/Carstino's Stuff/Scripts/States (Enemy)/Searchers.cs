using System.Collections.Generic;
using System.Collections;
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

    private NavMeshHit hit;
    public float sight = 10f;

    public GameObject enemy;
    public bool enemynear = false;

    public float viewRadius;
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    private void Start()
    {
        //OtherComponents
        agent = GetComponent<NavMeshAgent>();
        TestFood[] food = GameObject.FindObjectsByType<TestFood>(FindObjectsSortMode.None);
        Searchers[] search = GameObject.FindObjectsByType<Searchers>(FindObjectsSortMode.None);
        foods.AddRange(food);
        callFunctionClosestObject = false;
        Debug.Log(enemyObjects.myString);

        //StateMachine Initialization
        stateMachine = new MainEnemyState(this);
        stateMachine.Initialize(stateMachine.searchState);

        StartCoroutine("FindTargetsWithDelay", .2f);
    }

    private void Update()
    {
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

    public void FindNearestHideableObject()
    {
        Debug.Log("we ball");
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public void FindVisibleTargets()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask) && target.gameObject != gameObject)
                {
                    agent.SetDestination(target.transform.position);
                    Debug.Log("Enemy in sight");
                }
            }
        }
    }

    public IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }
}
