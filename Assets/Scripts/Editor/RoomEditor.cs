using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Room))]
[CanEditMultipleObjects]
public class RoomEditor : Editor
{
    SerializedProperty m_RoomObj;
    SerializedProperty m_VectorInt;

    private void OnEnable()
    {
        m_RoomObj = serializedObject.FindProperty("roomObj");
        m_VectorInt = serializedObject.FindProperty("gridDimensions");

    }

    public override void OnInspectorGUI()
    {

        //Room thisRoom = (Room)target;

        EditorGUILayout.ObjectField(m_RoomObj, new GUIContent("Room Object"));
        EditorGUILayout.PropertyField(m_VectorInt, new GUIContent("Grid Dimensions"));

        serializedObject.ApplyModifiedProperties();
    }
}
