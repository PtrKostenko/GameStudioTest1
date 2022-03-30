using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace GameStudioTest1.Editors
{
    [CustomEditor(typeof(BoxBorders)), CanEditMultipleObjects]
    public class BoxBordersEditor : Editor
    {
        private BoxBoundsHandle _boundsHandle = new BoxBoundsHandle();
        protected virtual void OnSceneGUI()
        {
            BoxBorders example = (BoxBorders)target;


            // copy the target object's data to the handle
            _boundsHandle.center = example.Borders.center;
            _boundsHandle.size = example.Borders.size;


            EditorGUI.BeginChangeCheck();
            _boundsHandle.DrawHandle();
            if (EditorGUI.EndChangeCheck())
            {
                // record the target object before setting new values so changes can be undone/redone
                Undo.RecordObject(example, "Change borders");

                example.Borders.center = _boundsHandle.center;
                example.Borders.size = _boundsHandle.size;
                
            }
        }
    }

}
