using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class TestDummyScript : MonoBehaviour
{
    public GameObject[] amountOfFood;
    private GameObject FoodAmount;
    public NavMeshAgent agent;
    public Transform planeVector;
    public float hunger = 50;
    private int foodInt;
    public Vector3 point;

    public float timeBeforeMoving;
    public float timeToMove = 3;

    public List<TestFood> foods = new List<TestFood>();

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        RandomPoint(planeVector.position, 10, out point);
        TestFood[] food = FindObjectsByType<TestFood>(FindObjectsSortMode.None);
        Debug.Log(food.Length); 
    }

    
    void Update()
    {
        hunger -= Time.deltaTime;

        if (hunger > 50)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                RandomPoint(planeVector.position, 25, out point);
                agent.SetDestination(point);
            }    
        }
        if (hunger < 50)
        {
            foreach (TestFood food in foods)
            {
                //float distanceChecker = Vector3.Distance(transform.position, GetComponent<TestFood>().transform.position);
                //Debug.Log(distanceChecker);
            }
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
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
