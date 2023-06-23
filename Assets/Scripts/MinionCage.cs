using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (damage < 0)
            damage = 0;
        currHealth -= damage;

        if (currHealth <= 0)
        {
            // 
            spawnedMinion.GetComponent<CapsuleCollider>().enabled = true;
            spawnedMinion.GetComponent<Minion>().enabled = true;
            spawnedMinion.transform.parent = null;
            Destroy(gameObject);
        }
    }
    void Start()
    {
        spawnedMinion = Instantiate(minionToSpawn, spawnPoint.transform.position, Quaternion.identity);
        spawnedMinion.GetComponent<Minion>().enabled = false;
        spawnedMinion.GetComponent<CapsuleCollider>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        
    }
}
