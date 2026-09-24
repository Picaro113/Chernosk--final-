using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Searchers))]
public class GUIvisioncone : Editor
{
    void OnSceneGUI()
    {
        Searchers searchers = (Searchers)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(searchers.transform.position, Vector3.up, Vector3.forward, 360, searchers.viewRadius);
        
        Vector3 viewAngleA = searchers.DirFromAngle(-searchers.viewAngle / 2, false);
        Vector3 viewAngleB = searchers.DirFromAngle(searchers.viewAngle / 2, false);

        Handles.DrawLine(searchers.transform.position, searchers.transform.position + viewAngleA * searchers.viewRadius);
        Handles.DrawLine(searchers.transform.position, searchers.transform.position + viewAngleB * searchers.viewRadius);
    }
}
