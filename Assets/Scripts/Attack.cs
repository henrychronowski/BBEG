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

    public float cooldownTimeInSeconds;

    public float totalTimeInSeconds;

    public GameObject hitboxPrefab;

    public GameObject activeHitbox;

    bool isProjectile;

    public Animation anim;

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
}
