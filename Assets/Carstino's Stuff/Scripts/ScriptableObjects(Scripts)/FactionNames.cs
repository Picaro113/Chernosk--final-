using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu(fileName = "Factions", menuName = "Factions/Factions")]
public class FactionNames : ScriptableObject
{
    public Searchers searchers;
    private bool seenEnemy;

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
        Debug.Log(searchers.factions);
    }

    public void CurrentFaction(Factions faction)
    {
        Factions currentFaction = faction;

        Factions dementors = Factions.Dementors;
        Factions forgotten = Factions.Forgotten;
        Factions ccc = Factions.CCC;
        Factions wanderers = Factions.Wanderers;

        if (currentFaction == Factions.Dementors)
        {
            Debug.Log("dementors");
            if (seenEnemy == true)
            {
                
            }
            else return;
        }

        //if (currentFaction == Factions.Forgotten)
        //{
        //    if (seenEnemy == true)
        //    {
        //        if (Factions.Dementors == dementors)
        //        {
        //            return;
        //        }
        //        if (Factions.Forgotten == forgotten)
        //        {
        //            return;
        //        }
        //        if (Factions.CCC == ccc)
        //        {
        //            searchers.seenEnemy = true;
        //        }
        //        if (Factions.Wanderers == wanderers)
        //        {
        //            searchers.seenEnemy = true;
        //        }
        //    }
        //    else return;
        //}
        //
        //if (currentFaction == Factions.CCC)
        //{
        //    if (seenEnemy == true)
        //    {
        //        if (Factions.Dementors == dementors)
        //        {
        //            return;
        //        }
        //        if (Factions.Forgotten == forgotten)
        //        {
        //            return;
        //        }
        //        if (Factions.CCC == ccc)
        //        {
        //            searchers.seenEnemy = true;
        //        }
        //        if (Factions.Wanderers == wanderers)
        //        {
        //            searchers.seenEnemy = true;
        //        }
        //    }
        //    else return;
        //}
        //
        //if (currentFaction == Factions.Wanderers)
        //{
        //    if (seenEnemy == true)
        //    {
        //        if (Factions.Dementors == dementors)
        //        {
        //            return;
        //        }
        //        if (Factions.Forgotten == forgotten)
        //        {
        //            return;
        //        }
        //        if (Factions.CCC == ccc)
        //        {
        //            searchers.seenEnemy = true;
        //        }
        //        if (Factions.Wanderers == wanderers)
        //        {
        //            searchers.seenEnemy = true;
        //        }
        //    }
        //    else return;
        //}
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
                        seenEnemy = true;
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

    public IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }
}
