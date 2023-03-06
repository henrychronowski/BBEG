using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Room))]
[CanEditMultipleObjects]
public class RoomEditor : Editor
{
    SerializedProperty m_RoomScene;
    private void OnEnable()
    {
        m_RoomScene = serializedObject.FindProperty("room");
    }

    public override void OnInspectorGUI()
    {

        Room thisRoom = (Room)target;

        EditorGUILayout.PropertyField(m_RoomScene, new GUIContent("Scene"), GUILayout.Height(20));

        serializedObject.ApplyModifiedProperties();
    }
}
