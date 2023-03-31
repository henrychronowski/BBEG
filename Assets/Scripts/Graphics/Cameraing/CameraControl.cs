using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    #if UNITY_EDITOR
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

    // Room-based variables (will be contained within a scriptable object for the room or something like that)
    private Transform lookAt;

    public void ChangeFocus(CameraFocus newFocus)
    {
        activeFocus = newFocus;
        lookAt = newFocus.transform;
        activeSettings = newFocus.GetFocusSettings();
    }

	private void Update()
	{
		if(tmp == true)
		{
            lookAt = activeFocus.transform;
            activeSettings = activeFocus.GetFocusSettings();
        }
	}

	void FixedUpdate()
    {
        if(lookAt != null)
		{
            targetPos = lookAt.position - activeSettings.GetOffset();
		}
    }

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
        lookAt = activeFocus.transform;
        activeSettings = activeFocus.GetFocusSettings();
    }

    void LateUpdate()
    {
        lastPos = Vector3.Lerp(lastPos, targetPos, activeSettings.GetSmoothSpeed() * Time.deltaTime);
        activeCamera.transform.position = lastPos;
    }
}
