using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Searchers : MonoBehaviour
{
    [Header("Scriptable Objects")]
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

    [Header("Detection for enemies")]
    public float viewRadius;
    public float viewAngle;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public bool seenEnemy;

    [Header("Gun functions")]
    public GameObject bulletAsGameObject;
    public Transform gunbarrel;
    public float timeToAttack = 3f;
    public float timeDelay;
    private float movetimer;
    public float loseplayertime = 5f;
    private float shottimer;
    private float firerate = 5f;

    [Header("Player references")]
    public GameObject Player;

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

                    //if (target.gameObject.CompareTag("Player"))
                    //{
                    //    Debug.Log("Hello this is the player");
                    //}
                    //else
                    //{
                    //    Debug.Log("This is not the player");
                    //}


                    //Searchers targetfaction = target.GetComponent<Searchers>();
                    //if (targetfaction != null && targetfaction != this.gameObject)
                    //{
                    //    if (factions.GetFaction(targetfaction.factions.myfaction) == true || factions.neutral == true)
                    //    {
                    //        if (factions.neutral == true && factions.GetFaction(targetfaction.factions.myfaction) == false)
                    //        {
                    //            Debug.Log("you're not my faction but we chill");
                    //        }
                    //        if (factions.GetFaction(targetfaction.factions.myfaction) == true)
                    //        {
                    //            Debug.Log("you're my faction");
                    //            Debug.Log(targetfaction.factions.myfaction);
                    //        }
                    //    }
                    //    else if (factions.GetFaction(targetfaction.factions.myfaction) == false || factions.neutral == false)
                    //    {   
                    //        if (factions.neutral == false)
                    //        {
                    //            Debug.Log("another faction");
                    //            Debug.Log(targetfaction.factions.myfaction);  
                    //        }
                    //    
                    //    }
                    //}
                }
                else
                {
                    seenEnemy = false;
                }
            }
        }
    }

    public void shoot()
    {
        //inst bullet
        GameObject bullet = GameObject.Instantiate(bulletAsGameObject, gunbarrel.position, transform.rotation);
        //calc dir to palyer
        Vector3 shootdir = (Player.transform.position - gunbarrel.position).normalized;
        //add force to rb
        bullet.GetComponent<Rigidbody>().linearVelocity = Quaternion.AngleAxis(Random.Range(-2f, 2f), Vector3.up) * shootdir * 55;
        Debug.Log("shoot");
        shottimer = 0;
    }

    public void Something()
    {
        //lock lose player timer and increment move and shoot timers
        loseplayertime = 0;
        movetimer += Time.deltaTime;
        shottimer += Time.deltaTime;
        transform.LookAt(Player.transform);
        //if shot timer > firerate
        if (shottimer > firerate)
        {
            shoot();
        }

        if (movetimer > Random.Range(2, 5)) //move to random position after random time
        {
            agent.SetDestination(transform.position + (Random.insideUnitSphere * 5));
            movetimer = 0;
        }

        transform.position = Player.transform.position;
        shoot();
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

