using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[CustomEditor(typeof(ArtifactBase))]
[CanEditMultipleObjects]
public class ArtifactEditor : Editor
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    SerializedProperty m_Artifact;

    private void OnEnable()
    {
        m_Artifact = serializedObject.FindProperty(nameof(ArtifactBase.affectedCharacterTypes));
    }

    public override void OnInspectorGUI()
    {

        ArtifactBase gen = (ArtifactBase)target;

        //GUILayout.BeginVertical();
        //EditorGUILayout.PropertyField(m_Artifact);
        

        base.OnInspectorGUI();
    }
}
