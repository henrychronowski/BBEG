using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactBehaviorBase : MonoBehaviour
{

    public virtual void OnCollect()
    {
        Debug.Log("No collect behavior");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
