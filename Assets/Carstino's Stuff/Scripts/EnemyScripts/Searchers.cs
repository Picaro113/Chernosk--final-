using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Searchers : MonoBehaviour
{
    //[Header("Scriptable Objects")]
    //public FactionNames factions;

    [Header("Ai")]
    public NavMeshAgent agent;
    public Transform planeVector;
    public float hunger = 50;
    public Vector3 point;
    public MainEnemyState stateMachine;

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
    public bool lostDuration;

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

        StartCoroutine(FindTargetsWithDelay(.2f));

        seenEnemy = false;
        lostDuration = false;
    }

    private void Update()
    {
        hunger -= Time.deltaTime;
        stateMachine.Update();

        if (lostDuration == true)
        {
            loseplayertime -= Time.deltaTime;

            if (loseplayertime <= 0)
            {
                seenEnemy = false;
                loseplayertime = 5;
            }
        }
        else
        {
            loseplayertime = 5;
        }

        Debug.Log(lostDuration + " this is lost duration");
        Debug.Log(seenEnemy + " this is the player seen");
        Debug.Log(loseplayertime + " this is losing the player");
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
            Player = target.gameObject;
            Vector3 dirToTarget = (Player.transform.position - transform.position).normalized;
            float dstToTarget = Vector3.Distance(transform.position, Player.transform.position);

            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask) && Player.gameObject != gameObject)
                {
                    if (target.gameObject.CompareTag("Player"))
                    {
                        Debug.Log("this is the player");
                        //seenEnemy = true;
                    }
                    else
                    {
                        Debug.Log("this is not the player");
                    }
                }
            }
            if (Vector3.Angle(transform.forward, dirToTarget) !< viewAngle / 2)
            {
                Debug.Log("out of view");
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

    public void Chase() {
        agent.SetDestination(Player.transform.position);
        //if (Vector3.Distance(transform.position, Player.transform.position) < )
        //{ 
        //    // attack
        //}
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

