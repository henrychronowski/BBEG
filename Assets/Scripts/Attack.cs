using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    Melee,
    Ranged
}

public struct HitData
{
    // This is to simplify AttackConnected event listeners so that functions being called from the event
    // can just take in a HitData object instead of two characters an attack and a float

    public HitData(Character owner, Character recipient, Attack attack, float damage)
    {
        mOwner = owner;
        mRecipient = recipient;
        mAttack = attack;
        mDamage = damage;
    }

    // Returns a processed damage value with the owner's attack and the recipient's defense taken into account
    // Used in PlayerCharacterManager::HitCharacter()
    // Does not directly modify mDamage
    // Rounds to nearest integer
    public int ProcessDamage()
    {
        if(mAttack.isProjectile)
            return Mathf.RoundToInt((mDamage + mOwner.GetRangedAffinity()) - mRecipient.GetDefense());
        else
            return Mathf.RoundToInt((mDamage + mOwner.GetMeleeAffinity()) - mRecipient.GetDefense());
    }

    public Character mOwner { get; private set; }
    public Character mRecipient { get; private set; }
    public Attack mAttack { get; private set; }
    public float mDamage { get; set; }
}

[CreateAssetMenu(fileName = "NewAttack", menuName = "ScriptableObjects/Create Attack", order = 1)]
[SerializeField]
public class Attack : ScriptableObject
{
    // animation to use
    public float startupInSeconds;

    public float activeTimeInSeconds;

    public float totalTimeInSeconds;

    public float damage;

    public int soundId;

    // How much to offset the hitbox spawn position from the player
    public float spawnOffset;

    public GameObject hitboxPrefab;

    public bool isProjectile;

    public AnimatorOverrideController animController;

    // (only applicable if ranged)
    public float projectileLifetime;

    public Hitbox GenerateHitbox(Character owner)
    {
        Hitbox h = Instantiate(hitboxPrefab).GetComponent<Hitbox>();
        h.Init(owner, this);
        
        return h;
    }

    private void OnEnable()
    {
        if(totalTimeInSeconds < startupInSeconds + activeTimeInSeconds)
        {
            Debug.LogError(name + " total time adjusted from " + totalTimeInSeconds
                + " to " + (startupInSeconds + activeTimeInSeconds));
            totalTimeInSeconds = startupInSeconds + activeTimeInSeconds;
        }
    }
}
