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
        m_RoomsList = null;
        m_RoomsList = serializedObject.FindProperty("roomArray");
        m_RoomWeights = serializedObject.FindProperty("roomWeight");
        m_RoomCount = serializedObject.FindProperty("count");
    }

    // THIS IS THE SET EDITOR, NOT THE ROOM EDITOR
    public override void OnInspectorGUI()
    {
        m_RoomCount.intValue = EditorGUILayout.IntField("Count", m_RoomCount.intValue);

        if (m_RoomCount.intValue != m_RoomsList.arraySize)
        {
            //m_RoomsList.ClearArray();
            //m_RoomWeights.ClearArray();
            RoomSet a = (RoomSet)target;
            a.Resize(m_RoomCount.intValue);
            serializedObject.ApplyModifiedProperties();
            Init();
        }
        EditorGUILayout.BeginHorizontal();

        if(GUILayout.Button("Add Room"))
        {
            m_RoomCount.intValue++;
            RoomSet a = (RoomSet)target;
            a.Resize(m_RoomCount.intValue);
            serializedObject.Update();
        }
        if (GUILayout.Button("Remove Room"))
        {
            m_RoomCount.intValue--;
            RoomSet a = (RoomSet)target;
            a.Resize(m_RoomCount.intValue);
            serializedObject.Update();
        }
        
        EditorGUILayout.EndHorizontal();
        EditorGUIUtility.labelWidth = 50f;
        //Room thisRoom = (Room)target;
        for (int i = 0; i < m_RoomCount.intValue; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.ObjectField(m_RoomsList.GetArrayElementAtIndex(i), new GUIContent("Room " + i.ToString()), GUILayout.Width(250f), GUILayout.ExpandWidth(false));
            
            m_RoomWeights.GetArrayElementAtIndex(i).intValue = 
                EditorGUILayout.IntField("Weight",m_RoomWeights.GetArrayElementAtIndex(i).intValue, GUILayout.Width(100f), GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

        }
        Debug.Log(" Final " + serializedObject.ApplyModifiedProperties());
        
    }
}
