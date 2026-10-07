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
            StartCoroutine(FindTargetsWithDelay(.2f));
        }

        seenEnemy = false;


        //Searching for faction
    }

    private void Update()
    {
        hunger -= Time.deltaTime;
        stateMachine.Update();
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

    public void FindVisibleTargets()
    {
        if (searchers != null)
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
                        seenEnemy = true;
                        Searchers targetfaction = target.GetComponent<Searchers>();
                        if (targetfaction != null && targetfaction != this.gameObject)
                        {
                            if (factions.GetFaction(targetfaction.factions.myfaction))
                            {
                                if (factions.neutral == false)
                                {
                                    Debug.Log("we chill for now");
                                }
                                else if (factions.neutral == true)
                                {
                                    Debug.Log("another faction");
                                    Debug.Log(targetfaction.factions.myfaction);
                                }
                            }
                            else if (!factions.GetFaction(targetfaction.factions.myfaction))
                            {
                                Debug.Log("you're my faction");
                                Debug.Log(targetfaction.factions.myfaction);
                            }
                        }

                    }
                    else
                    {
                        seenEnemy = false;
                    }
                }
            }
        }
        if (searchers == null)
        {
            Debug.Log("something is wrong");
        }
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
    public IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }
}
