using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Camera Settings", menuName = "Camera Settings", order = 1)]
public class CameraSettings : ScriptableObject
{
    [SerializeField] private string descriptor = "Default";
    [SerializeField] private float smoothSpeed = 10.0f;
    [SerializeField] private Vector3 offset = new Vector3(0.0f, -13.0f, 5.0f);

    public string GetDescriptor()
    {
        return descriptor;
    }

    public Vector3 GetOffset()
    {
        return offset;
    }

    public float GetSmoothSpeed()
    {
        return smoothSpeed;
    }
}
