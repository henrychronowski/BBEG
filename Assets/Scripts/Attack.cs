using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    LightMelee,
    LightRanged,
    HeavyMelee,
    HeavyRanged
}

[CreateAssetMenu(fileName = "NewAttack", menuName = "ScriptableObjects/Create Attack", order = 1)]
[SerializeField]
public class Attack : ScriptableObject
{
    // animation to use
    public float startupInSeconds;

    public float activeTimeInSeconds;

    public float totalTimeInSeconds;

    // How much to offset the hitbox spawn position from the player
    public float spawnOffset;

    public GameObject hitboxPrefab;

    public GameObject activeHitbox;

    bool isProjectile;

    public AnimationClip anim;

    public AnimatorOverrideController animController;

    // (only applicable if ranged)
    public float projectileLifetime;

    void Activate(Transform activeLocation)
    {
        activeHitbox = Instantiate(hitboxPrefab, activeLocation);
        //Animator animator = activeHitbox.GetComponent<Animator>().Play();
        //Rect hitbox = new Rect()
    }

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
