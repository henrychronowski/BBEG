using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Camera Settings", menuName = "Camera Settings", order = 1)]
public class CameraSettings : ScriptableObject
{
    [SerializeField,Tooltip("Just a name to describe the object with")] 
    private string descriptor = "Default";
    [SerializeField,Tooltip("General smoothing speed for camera movement on this focus")] 
    private float smoothSpeed = 10.0f;
    [SerializeField,Tooltip("The position of the camera relative to the position of the focus")] 
    private Vector3 offset = new Vector3(0.0f, -13.0f, 5.0f);

    // This function exists to make the "unused variable" warning go away
    public string GetDescriptor()
    {
        return descriptor;
    }

    // Returns the position of the camera relative to the position of the focus
    public Vector3 GetOffset()
    {
        return offset;
    }

    // Returns the general smoothing speed for camera movement
    public float GetSmoothSpeed()
    {
        return smoothSpeed;
    }
}
