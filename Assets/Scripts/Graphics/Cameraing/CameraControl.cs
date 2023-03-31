using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    #if UNITY_EDITOR
    [Tooltip("This is a bool that triggers a manual refresh of camera settings")]
    public bool tmp = false;
    #endif

    // Singleton components
    public static CameraControl Instance {get; private set;}

    // General variables
    private Camera activeCamera;    // Eventually maybe have an array of them and choose the active
    [SerializeField] CameraSettings activeSettings;
    public CameraFocus activeFocus;
    Vector3 lastPos;
    Vector3 targetPos;

    public void ChangeFocus(CameraFocus newFocus)
    {
        activeFocus = newFocus;
        activeSettings = newFocus.GetFocusSettings();
    }

    #if UNITY_EDITOR
	private void Update()
	{
		if(tmp == true)
		{
            activeSettings = activeFocus.GetFocusSettings();
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
