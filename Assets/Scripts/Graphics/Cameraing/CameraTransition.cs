using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class manages some camera transitions.
 * It is attached to a collider object, and when the player enters that collider it triggers a camera transition
 * to a new focus via the camera controller.
 */
public class CameraTransition : MonoBehaviour
{
    [SerializeField,Tooltip("The focus containing the camera settings for this area")]
    private CameraFocus AreaFocus;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            CameraControl.Instance.ChangeFocus(AreaFocus);
        }
    }
}
