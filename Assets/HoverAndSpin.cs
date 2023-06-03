using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverAndSpin : MonoBehaviour
{
    public float yRange;
    public float rotateSpeed;
    float initialYPos;
    // Start is called before the first frame update
    void Start()
    {
        initialYPos = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, rotateSpeed, 0));
        transform.position = new Vector3(transform.position.x, initialYPos + (Mathf.Sin(Time.time) * yRange), transform.position.z); 
    }
}
