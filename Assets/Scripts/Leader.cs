using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Leader : Character
{
    // PlayerCharacterManager sends input to this character

    // Passive ability, implement it like a unique artifact
    // Active ability
    public override void Hit(int damage)
    {
        if (damage < 0)
            damage = 0;
        currHealth -= damage;

        if (currHealth <= 0)
        {
            currHealth = 0;
            if (PlayerCharacterManager.instance.minions.Count == 0)
                EventManager.instance.DemoEndReached();
            else
                EventManager.instance.LeaderDeath();
        }
    }

    void SacrificeHeal(Minion m)
    {
        currHealth += m.currHealth;
        if(currHealth > GetMaxHealth())
        {
            currHealth = GetMaxHealth();
        }
    }
    private void Start()
    {
        state = new IdleState(this);
        EventManager.instance.onSacrificeMinion += SacrificeHeal;
    }

    private void OnDestroy()
    {
        EventManager.instance.onSacrificeMinion -= SacrificeHeal;
    }
}
