using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

[System.Serializable]
public class Leader : Character
{
    // PlayerCharacterManager sends input to this character

    // Passive ability, implement it like a unique artifact
    // Active ability

    float timeSinceLastHit = 0f;
    [SerializeField] float invulnTime = 0.5f;
    public override void Hit(int damage)
    {

        if (currHealth - damage <= 0)
        {
            currHealth = 0;
            if (PlayerCharacterManager.instance.minions.Count == 0)
                EventManager.instance.DemoEndReached();
            else
                EventManager.instance.LeaderDeath();
        }
        else base.Hit(damage);
    }

    void Update()
    {
        base.Update();
        //currentState = state.stateType;
        //state.Update();
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
        EventManager.instance.onHealReward += Heal;
    }

    private void OnDestroy()
    {
        EventManager.instance.onSacrificeMinion -= SacrificeHeal;
        EventManager.instance.onHealReward -= Heal;
    }
}
