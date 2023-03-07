using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // General variables
    [SerializeField]private Camera activeCamera;    // Eventually maybe have an array of them and choose the active
    Quaternion lastRot;
    Quaternion targetRot;

    [Header("Smoothing")]
    [SerializeField]private float smoothSpeed = 10.0f;

    // Room-based variables (will be contained within a scriptable object for the room or something like that)
    [SerializeField]private Transform lookAt;
    [SerializeField]private Transform targetPos;

    void FixedUpdate()
    {
        if(lookAt != null)
        {
            Debug.DrawLine(lookAt.position, activeCamera.transform.position, Color.gray);

            Vector3 difference = lookAt.position - activeCamera.transform.position;
            Quaternion rotation = Quaternion.LookRotation(difference, Vector3.up);
            targetRot = rotation;
        }
        else
        {
            targetRot = lastRot;
        }
    }

    void Awake()
    {
        lastRot = activeCamera.transform.rotation;
        targetRot = lastRot;
    }

    void LateUpdate()
    {
        lastRot = Quaternion.Slerp(lastRot, targetRot, smoothSpeed * Time.deltaTime);
        activeCamera.transform.rotation = lastRot;
    }
}
