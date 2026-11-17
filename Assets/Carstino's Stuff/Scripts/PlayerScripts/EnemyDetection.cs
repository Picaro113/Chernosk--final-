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
        Debug.Log(infront);

        Vector3 dirToTarget = Vector3.Normalize(enemy.transform.position - transform.position);

        if (Vector3.Dot(transform.forward, dirToTarget) > .707f)
        {
            infront = true;
        }
        if (Vector3.Dot(transform.forward, dirToTarget) < -.707f)
        {
            infront = false;
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
