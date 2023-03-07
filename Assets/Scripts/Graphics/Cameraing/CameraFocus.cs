using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An empty game object used as a focus point for the camera
public class CameraFocus : MonoBehaviour
{
    public CameraSettings roomSettings;

	private void Awake()
	{
		if(roomSettings == null)
		{
			roomSettings = new CameraSettings();
		}
	}
}
