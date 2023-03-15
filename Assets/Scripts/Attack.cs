using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAttack", menuName = "ScriptableObjects/Create Attack", order = 1)]
[SerializeField]
public class Attack : ScriptableObject
{
    // animation to use
    public float startupInSeconds;

    public float activeTimeInSeconds;

    public float cooldownTimeInSeconds;

    public GameObject hitboxPrefab;

    

    public GameObject activeHitbox;

    bool isProjectile;

    void Activate(Transform activeLocation)
    {
        activeHitbox = Instantiate(hitboxPrefab, activeLocation);

        //Rect hitbox = new Rect()
    }
}
