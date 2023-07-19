using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Extends from Character because Hitboxes can only hit Character type objects
public class MinionCage : Character
{
    [SerializeField] GameObject minionToSpawn;
    [SerializeField] GameObject spawnedMinion;
    [SerializeField] Transform spawnPoint;
    int cost = 1;

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    public override void Hit(int damage)
    {
        // ensure cages always take 3 hits
        damage = 1;

        if(PlayerCharacterManager.instance.keys < cost)
        {
            //play fail sound
            EventManager.instance.CageBreakFail();
            return;
        }

        currHealth -= damage;
        EventManager.instance.PlaySound("CageBreak");

        if (currHealth <= 0)
        {
            // 
            PlayerCharacterManager.instance.keys -= cost;
            spawnedMinion.GetComponent<Rigidbody>().isKinematic = false;
            spawnedMinion.GetComponent<CapsuleCollider>().enabled = true;
            spawnedMinion.GetComponent<Minion>().enabled = true;
            spawnedMinion.transform.parent = null;

            EventManager.instance.CageBreakSuccess(spawnedMinion.GetComponent<Minion>());
            Destroy(gameObject);
        }
    }
    void Start()
    {
        spawnedMinion = Instantiate(minionToSpawn, spawnPoint.transform.position, Quaternion.identity);
        spawnedMinion.transform.parent = this.gameObject.transform;
        spawnedMinion.GetComponent<Rigidbody>().isKinematic = true;
        spawnedMinion.GetComponent<CapsuleCollider>().enabled = false;
        spawnedMinion.GetComponent<Minion>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        
    }
}
