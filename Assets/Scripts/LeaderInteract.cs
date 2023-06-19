using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderInteract : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Minion")
        {
            Minion m = other.gameObject.GetComponent<Minion>();
            if (m.isStray)
            {
                m.isStray = false;
                PlayerCharacterManager.instance.AddMinion(m);
                m.transform.parent = null;
            }
        }
    }
}
