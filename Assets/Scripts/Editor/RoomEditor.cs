using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Room))]
[CanEditMultipleObjects]
public class RoomEditor : Editor
{
    SerializedProperty m_RoomObj;
    private void OnEnable()
    {
        m_RoomObj = serializedObject.FindProperty("roomObj");
    }

    public override void OnInspectorGUI()
    {

        //Room thisRoom = (Room)target;

        EditorGUILayout.ObjectField(m_RoomObj, new GUIContent("Room Object"));
        serializedObject.ApplyModifiedProperties();
    }
}
