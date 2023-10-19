using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(RewardSpawner))]
[CanEditMultipleObjects]
public class RewardSpawnerEditor : Editor
{
    SerializedProperty m_Weights;
    SerializedProperty m_RarityWeights;

    // Start is called before the first frame update
    void OnEnable()
    {
        m_Weights = serializedObject.FindProperty("weights");
        m_RarityWeights = serializedObject.FindProperty("rarityWeights");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        string[] weightLabels = { "Gold", "Healing", "Key", "Artifact", "Minion" };
        EditorGUILayout.LabelField("Reward Weights");
        int totalWeight = 0;
        for(int i = 0; i < m_Weights.arraySize; i++)
        {
            EditorGUILayout.BeginHorizontal();
            SerializedProperty weight = m_Weights.GetArrayElementAtIndex(i);
            weight.intValue = EditorGUILayout.IntField(weightLabels[i], m_Weights.GetArrayElementAtIndex(i).intValue, GUILayout.Width(300f), GUILayout.ExpandWidth(false));
            EditorGUILayout.EndHorizontal();
            totalWeight += weight.intValue;
        }
        EditorGUILayout.LabelField("Total Weight: " + totalWeight.ToString());
        if(totalWeight != 100)
        {
            EditorGUILayout.LabelField("Try to keep total weight at 100 so that a weight of 1 = 1%, etc.");

        }
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Rarity Weights");
        int totalRarityWeight = 0;
        for (int i = 0; i < m_RarityWeights.arraySize; i++)
        {
            EditorGUILayout.BeginHorizontal();
            SerializedProperty weight = m_RarityWeights.GetArrayElementAtIndex(i);
            weight.intValue = EditorGUILayout.IntField(((Rarity)i).ToString(), m_RarityWeights.GetArrayElementAtIndex(i).intValue, GUILayout.Width(300f), GUILayout.ExpandWidth(false));
            EditorGUILayout.EndHorizontal();
            totalRarityWeight += weight.intValue;
        }
        EditorGUILayout.LabelField("Total Weight: " + totalWeight.ToString());
        if (totalRarityWeight != 100)
        {
            EditorGUILayout.LabelField("Try to keep total weight at 100 so that a weight of 1 = 1%, etc.");

        }


        serializedObject.ApplyModifiedProperties();

        
    }
}
