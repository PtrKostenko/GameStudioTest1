using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;
using GameStudioTest1.AI;

namespace GameStudioTest1.Editors
{
    [CustomEditor(typeof(Route))]
    class RouteEditor : Editor
    {
        protected virtual void OnSceneGUI()
        {
            Route example = (Route)target;

            if (example.RoutePoints is null)
                return;
                EditorGUI.BeginChangeCheck();

                Vector3[] positionHandles = new Vector3[example.RoutePoints.Length];

            for (int i = 0; i < example.RoutePoints.Length; i++)
            {
                positionHandles[i] = Handles.PositionHandle(example.RoutePoints[i].Position, Quaternion.identity);


                //drawing line
                Vector3 thisPos = example.RoutePoints[i].Position;
                Vector3 nextPos;
                if (i < example.RoutePoints.Length - 1)
                {
                    nextPos = example.RoutePoints[i + 1].Position;
                }
                else if (example.IsLooped)
                {
                    nextPos = example.RoutePoints[0].Position;
                }
                else
                {
                    nextPos = thisPos;
                }
                Handles.DrawLine(thisPos, nextPos);
                //


                    
            }

            if (EditorGUI.EndChangeCheck())
            {
                // record the target object before setting new values so changes can be undone/redone
                Undo.RecordObject(example, "Change route");

                for (int i = 0; i < example.RoutePoints.Length; i++)
                {
                    example.RoutePoints[i].Position = positionHandles[i];
                }

            }
        }
    }
}
