using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public int startingHealth; //enemy health
    public int damage; //damage done by enemy

    public float attackRecharge; //how long before enemy can attack after attacking
    public float attackDistance; //how close ai needs to be to player to attack

    public GameObject weapon; //will be parented in enemy model as well

    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AttackPlayer()
    {
        //check if the player is in attack range
        //if (gameObject.transform.position)
    }

    public void DamageTaken(int damageReceived)
    {
        currentHealth -= damageReceived;

        if (currentHealth <= 0)
        {
            Destroy(gameObject); //destroy or deactivate or whatever
        }
    }
}