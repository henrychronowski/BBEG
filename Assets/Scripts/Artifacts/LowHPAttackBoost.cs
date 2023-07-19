using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowHPAttackBoost : ArtifactBehaviorBase
{
    public float healthPercentageRequirement = .2f;
    public float damageModifier = 2f;
    void ApplyAttackBoost(HitData hit)
    {
        float healthPercentage = hit.mOwner.currHealth / hit.mOwner.GetMaxHealth();

        if(healthPercentage < healthPercentageRequirement)
        {
            // Magic number for now, need some kind of way to set variables for these externally
            hit.mDamage *= damageModifier;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        EventManager.instance.onAttackConnected += ApplyAttackBoost;
    }

    private void OnDestroy()
    {
        EventManager.instance.onAttackConnected -= ApplyAttackBoost;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
