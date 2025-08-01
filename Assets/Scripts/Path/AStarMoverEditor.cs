using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AStarMover))]
public class AStarMoverEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        AStarMover mover = (AStarMover)target;

        if (mover.pathList != null && mover.pathList.Count > 0)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("A* Path Node Info", EditorStyles.boldLabel);

            foreach (var node in mover.pathList)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.Vector3Field("Position", node.pos);
                EditorGUILayout.FloatField("G (Cost)", node.nodeTotalCost);
                EditorGUILayout.FloatField("H (Heuristic)", node.estimateCost - node.nodeTotalCost);
                EditorGUILayout.FloatField("F (Total)", node.estimateCost);
                EditorGUILayout.EndVertical();
            }
        }
    }
}
