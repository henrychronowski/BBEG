using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : MonoBehaviour
{
    public Transform camera;
    private void LateUpdate()
    {
        Vector3 newPosition = camera.position;
        newPosition.y = transform.position.y;
        newPosition.z += 5;
        transform.position = newPosition;
    }
}
