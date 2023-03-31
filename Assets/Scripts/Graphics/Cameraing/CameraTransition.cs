using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    [SerializeField]
    private CameraFocus AreaFocus;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            CameraControl.Instance.ChangeFocus(AreaFocus);
        }
    }
}
