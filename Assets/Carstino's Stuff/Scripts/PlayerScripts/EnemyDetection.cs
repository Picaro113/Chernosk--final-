using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public GameObject enemy;
    public bool infront;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        infront = false;   
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dirToTarget = Vector3.Normalize(enemy.transform.position - transform.position);

        if (Vector3.Dot(transform.forward, dirToTarget) > .707f)
        {
            infront = true;
        }
        else
        {
            infront = false;
        }
    }
}
