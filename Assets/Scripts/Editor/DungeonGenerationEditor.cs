using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DungeonGeneration))]
[CanEditMultipleObjects]
public class DungeonGenerationEditor : Editor
{
    SerializedProperty m_DungeonGeneration;

    private void OnEnable()
    {
        m_DungeonGeneration = serializedObject.FindProperty("m_DungeonGeneration");
    }

    public override void OnInspectorGUI()
    {

        DungeonGeneration gen = (DungeonGeneration)target;

        if(GUILayout.Button("Generate"))
        {
            gen.Clear();
            gen.Generate();
        }

        if (GUILayout.Button("Clear"))
        {
            gen.Clear();
        }

        base.OnInspectorGUI();
    }

}
