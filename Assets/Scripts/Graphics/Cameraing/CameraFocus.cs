using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An empty game object used as a focus point for the camera, it can be attached
//  to a character or just left in the world
public class CameraFocus : MonoBehaviour
{
	[SerializeField]
    private CameraSettings roomSettings;

	private void Awake()
	{
		if(roomSettings == null)
		{
			roomSettings = CameraSettings.CreateInstance<CameraSettings>();
		}
	}

	// Returns the camera settings for this focus
	public CameraSettings GetFocusSettings()
	{
		return roomSettings;
	}
}
