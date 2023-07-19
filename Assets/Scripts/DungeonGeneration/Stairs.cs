using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    [SerializeField] bool used = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !used)
        {
            EventManager.instance.StairsReached();
            used = true;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !used)
        {
            EventManager.instance.StairsReached();
            used = true;
        }
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
