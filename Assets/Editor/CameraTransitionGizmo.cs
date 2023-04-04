using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CameraTransitionGizmo : MonoBehaviour
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Pickable)]
    static void DrawTransitionZone(CameraTransition zone, GizmoType gizmoType)
    {
        BoxCollider collider = zone.gameObject.GetComponent<BoxCollider>();
        //Gizmos.color = Color.green;
        //Gizmos.DrawWireCube(zone.transform.position + collider.center, collider.size);
        Gizmos.DrawIcon(zone.transform.position + collider.center, "collaborate-dark@2x", true);
    }
}
