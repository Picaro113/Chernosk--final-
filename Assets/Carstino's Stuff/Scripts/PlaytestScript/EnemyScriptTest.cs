using UnityEngine;
using UnityEngine.AI;

public class EnemyScriptTest : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 point;
    public GameObject Player;
    public GameObject bullet;
    public Transform gunbarrel;
    public float shottimer;
    public float firerate = 3f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        shottimer = 0;
        firerate = 3;
    }

    void Update()
    {
        RandomPoint(transform.position, 100, out point);
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(point);
        }

        Something();
    }

    public void shoot()
    {
        //inst bullet
        bullet = GameObject.Instantiate(bullet, gunbarrel.position, transform.rotation);
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
        shottimer += Time.deltaTime;
        transform.LookAt(Player.transform);
        //if shot timer > firerate
        if (shottimer > firerate)
        {
            shoot();
        }
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
}
