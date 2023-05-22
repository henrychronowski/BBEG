using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLabel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TMPro.TextMeshPro>().text = transform.parent.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
