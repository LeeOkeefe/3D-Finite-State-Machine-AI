using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(Waypoint))]
    public class WaypointCustomEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var myTarget = (Waypoint) target;

            if (GUILayout.Button("Calculate Linked Nodes"))
            {
                myTarget.CalculateNLinkedNodes();
            }
        }
    }
}
