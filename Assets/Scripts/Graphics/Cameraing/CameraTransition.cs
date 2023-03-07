using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Camera Transition", menuName = "Camera Transition", order = 1)]
public class CameraTransition : ScriptableObject
{
    public string TransitionName;

    // Will fill out types of transition here at some point in the future
}
