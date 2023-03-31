using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This is the overarching camera singleton. It interacts with camera focuses and transitions
 * in order to manage all camera functions. Anything not within the camera system should only
 * interact with it via this singleton or in-world camera transitions.
 */
public class CameraControl : MonoBehaviour
{
    #if UNITY_EDITOR
    [Tooltip("This is a bool that triggers a manual refresh of camera settings")]
    public bool RefreshValues = false;
    #endif

    // Singleton components
    public static CameraControl Instance {get; private set;}

    // General variables
    private Camera activeCamera;    // The currently active camera; Eventually maybe have an array of them and choose the active
    [SerializeField,Tooltip("The currently active camera settings, runtime set in code but exposed here for testing")] 
    private CameraSettings activeSettings;
    [SerializeField,Tooltip("The currently active camera focus, runtime set in code but exposed here for testing")] 
    private CameraFocus activeFocus;
    
    private Vector3 lastPos; // Position of the active camera on the previous frame
    private Vector3 targetPos; // WS position that the camera should be at

    // This shifts the focus of the camera and refreshes the active camera settings
    public void ChangeFocus(CameraFocus newFocus)
    {
        activeFocus = newFocus;
        activeSettings = newFocus.GetFocusSettings();
    }

    #if UNITY_EDITOR
	private void Update()
	{
		if(RefreshValues == true)
		{
            activeSettings = activeFocus.GetFocusSettings();
            RefreshValues = false;
        }
	}
    #endif

    /* this would shift the burden of checking the position into the physics loop, I don't think it's necessary
     * for now, cause might be more performance heavy than getting it in lateUpdate and we probably won't have
     * many moving objects, but if we notice stuttering with moving objects replace the call to GetWS in Late
     * Update with targetPos and uncomment this
    */
	// void FixedUpdate()
    // {
    //     targetPos = activeFocus.GetCameraPositionWS();
    // }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        activeCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        lastPos = activeCamera.transform.position;
        targetPos = lastPos;
    }

	private void Start()
	{
        activeSettings = activeFocus.GetFocusSettings();
    }

    void LateUpdate()
    {
        lastPos = Vector3.Lerp(lastPos, activeFocus.GetCameraPositionWS(), activeSettings.GetSmoothSpeed() * Time.deltaTime);
        activeCamera.transform.position = lastPos;
    }
}
