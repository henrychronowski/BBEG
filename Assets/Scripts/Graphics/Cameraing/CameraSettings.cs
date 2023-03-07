using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Camera Settings", menuName = "Camera Settings", order = 1)]
public class CameraSettings : ScriptableObject
{
    [SerializeField] private string name = "Default";
    public float smoothSpeed = 10.0f;
    public Vector3 offset = new Vector3(0.0f, -13.0f, 5.0f);
}
