using System.Collections;
using UnityEngine;
using UnityEditor;

namespace GameStudioTest1.Editors
{
    [CustomEditor(typeof(Level))]
    public class LevelEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var level = (Level)target;

            base.OnInspectorGUI();
            //if (GUILayout.Button("Restart"))
            //{
            //    level.RestartRequested?.Invoke();
            //}
            //if (GUILayout.Button("ExitToMainMenu"))
            //{
            //    level.ExitToMainMenuRequested?.Invoke();
            //}
        }
    }
}