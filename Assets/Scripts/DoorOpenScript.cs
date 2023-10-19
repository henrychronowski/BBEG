using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DoorOpenScript : MonoBehaviour
{
    public GameObject rotationPoint;
    public float rotationTime;
    public bool isRotating = false;
    public bool isOpen = false;
    public float elapsedTime;
    
    void Rotate()
    {
        elapsedTime += Time.deltaTime;

        Quaternion target = Quaternion.Euler(0, 90, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, (Time.deltaTime * 4) * (elapsedTime/rotationTime));

        if (elapsedTime >= rotationTime)
        {
            isRotating = false;
            isOpen = true;
        }
    }

    [YarnCommand("rotate")]
    void StartRotate()
    {
        isRotating = true;
    }

    void Update()
    {
        if(isRotating && !isOpen)
        {
            Rotate();
        }
    }
}
