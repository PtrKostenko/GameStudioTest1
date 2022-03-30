using System.Collections;
using UnityEngine;
using UnityEditor;
using GameStudioTest1.AI;

namespace GameStudioTest1.Editors
{
    [CustomEditor(typeof(SectorSense))]
    public class SectorSenseEditor : Editor
    {
        private void OnSceneGUI()
        {
            SectorSense edited = (SectorSense)target;
            if (edited is null)
                return;
            Vector3 pos = edited.transform.position;
            float angle = edited.Angle;
            Vector3 forward = edited.transform.forward;
            float radius = edited.Radius;

            Color col = Color.red;
            col.a = 0.5f;
            Handles.color = col;
            Handles.DrawSolidArc(pos, Vector2.up, Quaternion.Euler(0, -angle / 2f, 0) * forward, angle, radius);
            Handles.color = Color.black;
            Handles.DrawLine(pos, pos + forward * radius);
        }
    }
}