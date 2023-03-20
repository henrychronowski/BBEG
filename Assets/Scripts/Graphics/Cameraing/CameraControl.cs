using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // General variables
    private Camera activeCamera;    // Eventually maybe have an array of them and choose the active
    //Quaternion lastRot;
    //Quaternion targetRot;
    Vector3 lastPos;
    Vector3 targetPos;

    [Header("Smoothing")]
    [SerializeField] private float smoothSpeed = 10.0f;

    // Room-based variables (will be contained within a scriptable object for the room or something like that)
    [SerializeField] private Transform lookAt;
    [SerializeField] private Vector3 offset = new Vector3(0.0f, 13.0f, 5.0f);
    //[SerializeField]private Transform targetPos;

    void FixedUpdate()
    {
  //      if(lookAt != null)
  //      {
		//	Debug.DrawLine(lookAt.position, activeCamera.transform.position, Color.gray);

		//	Vector3 difference = lookAt.position - activeCamera.transform.position;
		//	Quaternion rotation = Quaternion.LookRotation(difference, Vector3.up);
		//	targetRot = rotation;
		//}
  //      else
  //      {
  //          targetRot = lastRot;
  //      }

        if(lookAt != null)
		{
            //Debug.DrawLine(lookAt.position - activeCamera.transform.position, activeCamera.transform.position, Color.gray);

            targetPos = lookAt.position - offset;
		}
    }

    void Awake()
    {
        activeCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        //lastRot = activeCamera.transform.rotation;
        //targetRot = lastRot;

        lastPos = activeCamera.transform.position;
        targetPos = lastPos;
    }

    void LateUpdate()
    {
        //lastRot = Quaternion.Slerp(lastRot, targetRot, smoothSpeed * Time.deltaTime);
        //activeCamera.transform.rotation = lastRot;
        lastPos = Vector3.Lerp(lastPos, targetPos, smoothSpeed * Time.deltaTime);
        activeCamera.transform.position = lastPos;
    }
}