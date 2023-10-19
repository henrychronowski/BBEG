using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitNumberSpawner : MonoBehaviour
{

    public GameObject numberObject;

    public void SpawnHitNumber(HitData hit)
    {
        if (hit.mRecipient.tag == "Breakable")
            return;

        GameObject hitNumber = Instantiate(numberObject, null);
        hitNumber.transform.position = hit.mRecipient.transform.position;
        hitNumber.GetComponent<HitNumber>().SetValue((int)hit.mDamage);
    }
    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.onHitProcessed += SpawnHitNumber;
    }

    private void OnDestroy()
    {
        EventManager.instance.onHitProcessed -= SpawnHitNumber;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
