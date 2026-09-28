using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class TestDummyScript : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform planeVector;
    public float hunger = 50;
    public Vector3 point;

    public float timeBeforeMoving;
    public float timeToMove = 3;

    public List<TestFood> foods = new List<TestFood>();

    public bool callFunctionClosestObject;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        TestFood[] food = GameObject.FindObjectsByType<TestFood>(FindObjectsSortMode.None);
        foods.AddRange(food);
        callFunctionClosestObject = false;
    }

    
    void Update()
    {
        hunger -= Time.deltaTime;
        if (hunger > 50)
        {
            callFunctionClosestObject = false;
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                RandomPoint(planeVector.position, 25, out point);
                agent.SetDestination(point);
            }    
        }
        if (hunger < 50 && callFunctionClosestObject == false)
        {
            Debug.Log("Hi");
            closestObject();
        }
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
