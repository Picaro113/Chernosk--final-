using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu(fileName = "Factions", menuName = "Factions/Factions")]
public class FactionNames : ScriptableObject
{
    private Searchers searchers;

    public enum Factions
    {
        Dementors,
        Forgotten,
        CCC,
        Wanderers
    }

    public Factions factions;

    private void Awake()
    {
        if (searchers == null)
        {
            searchers = GameObject.FindFirstObjectByType<Searchers>();
        }
    }

    public void CheckEnemyFactions()
    {
        Debug.Log("hello");
    }

    public void FindVisibleTargets()
    {
        if (searchers != null)
        {
            Collider[] targetsInViewRadius = Physics.OverlapSphere(searchers.transform.position, searchers.viewRadius, searchers.targetMask);

            for (int i = 0; i < targetsInViewRadius.Length; i++)
            {
                Transform target = targetsInViewRadius[i].transform;
                Vector3 dirToTarget = (target.position - searchers.transform.position).normalized;
                if (Vector3.Angle(searchers.transform.forward, dirToTarget) < searchers.viewAngle / 2)
                {
                    float dstToTarget = Vector3.Distance(searchers.transform.position, target.position);

                    if (!Physics.Raycast(searchers.transform.position, dirToTarget, dstToTarget, searchers.obstacleMask) && target.gameObject != searchers.gameObject)
                    {
                        Debug.Log("enemies have been found");
                        searchers.seenEnemy = true;
                    }
                    else
                    {
                        Debug.Log("no enemies have been found");
                        searchers.seenEnemy = false;
                    }
                }
            }
        }
        if (searchers == null)
        {
            Debug.Log("something is wrong");
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
