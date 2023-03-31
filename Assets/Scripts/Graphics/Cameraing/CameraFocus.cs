using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An empty game object used as a focus point for the camera, it can be attached
//  to a character or just left in the world
public class CameraFocus : MonoBehaviour
{
	[SerializeField,Tooltip("The settings that apply to this focus's area of effect")]
    private CameraSettings roomSettings;
	[SerializeField,Tooltip("Check True if the focus moves, false if it's a stationary object")]
	private bool isMobile = false;
	
	private Vector3 cameraPosOS; // The camera position relative to the focus transform
	private Vector3 cameraPosWS; // The camera position in world space

	private void Awake()
	{
		if(roomSettings == null)
		{
			roomSettings = CameraSettings.CreateInstance<CameraSettings>();
		}

		cameraPosOS = roomSettings.GetOffset();
		cameraPosWS = this.transform.position - cameraPosOS;
	}

	// Returns the camera settings for this focus
	public CameraSettings GetFocusSettings()
	{
		return roomSettings;
	}

	// Returns the camera position for this focus relative to the focus
	public Vector3 GetCameraPositionRelative()
	{
		return cameraPosOS;
	}

	// Returns the camera position for this focus in world space
	public Vector3 GetCameraPositionWS()
	{
		if(isMobile)
		{
			cameraPosWS = this.transform.position - cameraPosOS;
		}

		return cameraPosWS;
	}
}
