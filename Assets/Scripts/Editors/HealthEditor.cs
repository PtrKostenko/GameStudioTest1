using UnityEditor;
using UnityEngine;

namespace GameStudioTest1.Editors
{
    [CustomEditor(typeof(Health))]
    public class HealthEditor: Editor
    {
        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();
            Health health = (Health)target;
            health.MaxValue = (EditorGUILayout.IntField("Max health", health.MaxValue));
            health.Set(EditorGUILayout.IntSlider("Current health", health.CurrentValue, 0, health.MaxValue));
            ProgressBar((float)health.CurrentValue / (float)health.MaxValue, $"Health {health.CurrentValue}/{health.MaxValue}");
        }

        // Custom GUILayout progress bar.
        void ProgressBar(float value, string label)
        {
            // Get a rect for the progress bar using the same margins as a textfield:
            Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
            EditorGUI.ProgressBar(rect, value, label);
            EditorGUILayout.Space();
        }

    }
}