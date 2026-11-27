using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class Searchers : MonoBehaviour
{
    [Header("Ai")]
    public NavMeshAgent agent;
    public Transform planeVector;
    public float hunger = 50;
    public Vector3 point;
    public MainEnemyState stateMachine;

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
        Searchers[] search = GameObject.FindObjectsByType<Searchers>(FindObjectsSortMode.None);

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
        Debug.Log("currentstate is" + stateMachine.CurrentState);
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

        if (seenEnemy == true)
        {
            shottimer += Time.deltaTime;
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
    //public TestFood closestObject()
    //{
    //    TestFood closestTarget = null;
    //    float closestDistanceSqr = float.MaxValue;
    //
    //    foreach (TestFood food in foods)
    //    {
    //        if (food == null) continue;
    //
    //        Vector3 directionToTarget = food.transform.position - transform.position;
    //        float dSqrToTarget = directionToTarget.sqrMagnitude;
    //
    //        if (dSqrToTarget < closestDistanceSqr)
    //        {
    //            closestDistanceSqr = dSqrToTarget;
    //            closestTarget = food;
    //        }
    //    }
    //    if (closestTarget != null)
    //    {
    //        agent.SetDestination(closestTarget.transform.position);
    //    }
    //    return closestTarget;
    //}
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
        if (Vector3.Distance(transform.position, Player.transform.position) < viewRadius)
        {
            Vector3 targetdir = Player.transform.position - transform.position;
            float angleto = Vector3.Angle(targetdir, transform.forward);
            if (angleto >= -viewAngle && angleto <= viewAngle)
            {
                Ray ray = new Ray(transform.position, targetdir);
                RaycastHit hitinfo = new RaycastHit();

                if (Physics.Raycast(ray, out hitinfo, viewRadius))
                {
                    if (hitinfo.transform.gameObject == Player)
                    {
                        Debug.DrawRay(ray.origin, ray.direction * viewRadius);
                        seenEnemy = true;
                    }
                }
            }
        }
        else
        {
            seenEnemy = false;
        }


        /// Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        // for (int i = 0; i < targetsInViewRadius.Length; i++)
        // {
        //     Transform target = targetsInViewRadius[i].transform;
        //     if (targetsInViewRadius[i].gameObject.CompareTag("Player"))
        //     {
        //         Debug.Log("Player is in the range view");
        //     }
        //     Player = target.gameObject;
        //     Vector3 dirToTarget = (Player.transform.position - transform.position).normalized;
        //     float dstToTarget = Vector3.Distance(transform.position, Player.transform.position);
        //
        //
        //    
        //
        //
        //
        //     if (Vector3.Angle(transform.forward, dirToTarget) <= viewAngle / 2)
        //     {
        //         Ray ray = new Ray(transform.position, dirToTarget);
        //         if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask) && Player.gameObject != gameObject)
        //         {
        //             if (target.gameObject.CompareTag("Player"))
        //             {
        //                 Debug.Log("this is the player in view angle");
        //                 seenEnemy = true;
        //                 Debug.DrawRay(ray.origin, ray.direction * viewRadius);
        //             }
        //         }
        //     }
        //     if (Vector3.Angle(transform.forward, dirToTarget) > viewAngle / 2)
        //     {
        //         Ray ray = new Ray(transform.position, dirToTarget);
        //         if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask) && Player.gameObject != gameObject)
        //         {
        //             if (target.gameObject.CompareTag("Player"))
        //             {
        //                 Debug.Log("this is the player out of view angle");
        //                 seenEnemy = false;
        //                
        //                 Debug.DrawRay(ray.origin, ray.direction * viewRadius);
        //             }
        //         }
        //     }
        //
        //
        //
        //
        // }
        // if(targetsInViewRadius.Length == 0)
        // {
        //     seenEnemy = false;
        //     Debug.Log("no enemies");
        // }
        //



    }

    public void shoot()
    {
        //inst bullet
        GameObject bullet = GameObject.Instantiate(bulletAsGameObject, gunbarrel.position, transform.rotation);
        //calc dir to palyer
        Vector3 shootdir = (Player.transform.position - gunbarrel.position).normalized;
        //add force to rb
        bullet.GetComponent<Rigidbody>().linearVelocity = Quaternion.AngleAxis(Random.Range(-2f, 2f), Vector3.up) * transform.forward * 55;
        Debug.Log("shoot");
        shottimer = 0;
    }

    public void Chase() 
    {
        //float rand = Random.Range(1, 3);
        //Vector3 pos = new Vector3(Player.transform.position.x + rand, Player.transform.position.y + rand, Player.transform.position.z);
        agent.SetDestination(Player.transform.position);
    }

    public void Something()
    {
        //lock lose player timer and increment move and shoot timers
        loseplayertime = 0;
        movetimer += Time.deltaTime;
        shottimer += Time.deltaTime;
        if (shottimer > firerate)
        {
            shoot();
            shottimer = 0;
        }

        //if (movetimer > Random.Range(2, 5)) //move to random position after random time
        //{
        //    agent.SetDestination(transform.position + (Random.insideUnitSphere * 5));
        //    movetimer = 0;
        //}
        //
        //transform.position = Player.transform.position;
        //shoot();
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

