using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Searchers : MonoBehaviour
{
    [Header("Scriptable Objects")]
    public EnemyObjects enemyObjects;
    public FactionNames factions;

    [Header("StateMachine")]
    public MainEnemyState stateMachine;

    [Header("Ai")]
    public NavMeshAgent agent;
    public Transform planeVector;
    public float hunger = 50;
    public Vector3 point;

    [Header("Lists")]
    public List<TestFood> foods = new List<TestFood>();
    public List<Searchers> searchers = new List<Searchers>();

    [Header("Detection for enemies")]
    public float viewRadius;
    public float viewAngle;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public bool seenEnemy;

    [Header("Gun functions//testing purposes")]
    public GameObject bullet;
    public Transform gun;
    public float timeToAttack = 3f;
    public float timeDelay;

    private void Start()
    {
        //OtherComponents
        agent = GetComponent<NavMeshAgent>();
        TestFood[] food = GameObject.FindObjectsByType<TestFood>(FindObjectsSortMode.None);
        Searchers[] search = GameObject.FindObjectsByType<Searchers>(FindObjectsSortMode.None);
        foods.AddRange(food);

        //StateMachine Initialization
        stateMachine = new MainEnemyState(this);
        stateMachine.Initialize(stateMachine.patrollState);
        if (factions != null)
        {
            Debug.Log("faction couroutine is starting");
            StartCoroutine(factions.FindTargetsWithDelay(.2f));
        }

        seenEnemy = false;


        //Searching for faction

        if (factions == null)
        {
            Debug.Log("Could not find a faction");
        }
        else
        {
            Debug.Log("faction has been found" + factions.factions);
        }
    }

    private void Update()
    {
        hunger -= Time.deltaTime;
        stateMachine.Update();
        if (factions != null)
        {
            factions.CheckEnemyFactions();
        }
    }

    //this is finding a randompoint throughout the map
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

    //finds the closest object (food) from the current gameObject this script is on
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
        return closestTarget;
    }
    //makes a cone to calculate if something is infront of it
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public void shoot()
    {
        timeDelay += Time.deltaTime;

        if (timeDelay > timeToAttack)
        {
            Instantiate(bullet, gun.position, gun.rotation);
            timeDelay = 0;
        }
    }
}
