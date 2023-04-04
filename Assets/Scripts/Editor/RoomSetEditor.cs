using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoomSet))]
[CanEditMultipleObjects]
public class RoomSetEditor : Editor
{
    SerializedProperty m_RoomsList;
    SerializedProperty m_RoomCount;
    SerializedProperty m_RoomWeights;
    private void OnEnable()
    {
        Init();
    }

    void Init()
    {
        m_RoomsList = serializedObject.FindProperty("roomArray");
        m_RoomWeights = serializedObject.FindProperty("roomWeight");
        m_RoomCount = serializedObject.FindProperty("count");
    }

    // THIS IS THE SET EDITOR, NOT THE ROOM EDITOR
    public override void OnInspectorGUI()
    {
        m_RoomCount.intValue = EditorGUILayout.IntField("Count", m_RoomCount.intValue);

        if(m_RoomCount.intValue != m_RoomsList.arraySize)
        {
            m_RoomsList.ClearArray();
            RoomSet a = (RoomSet)target;
            a.Resize(m_RoomCount.intValue);
        }

        //Room thisRoom = (Room)target;
        for (int i = 0; i < m_RoomsList.arraySize; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.ObjectField(m_RoomsList.GetArrayElementAtIndex(i), new GUIContent("Room " + i.ToString()));

            m_RoomWeights.GetArrayElementAtIndex(i).intValue = 
                EditorGUILayout.IntField("Weight",m_RoomWeights.GetArrayElementAtIndex(i).intValue);

            EditorGUILayout.EndHorizontal();

        }
        serializedObject.ApplyModifiedProperties();
    }
}
