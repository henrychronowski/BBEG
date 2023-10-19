using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBuff", menuName = "ScriptableObjects/New Buff", order = 1)]
[SerializeField]
public class Buff : ScriptableObject
{
    public int maxHealthBuff;
    public float movementSpeedBuff;
    public int meleeAttackBuff;
    public int rangedAttackBuff;
    public int defenseBuff;
    public float uncommonMultiplier = 1.5f;
    public float rareMultiplier = 2f;
    public Rarity rarity;

    public float GetMultiplier()
    {
        switch(rarity)
        {
            case Rarity.Common:
                {
                    return 1f;
                }
            case Rarity.Uncommon:
                {
                    return uncommonMultiplier;
                }
            case Rarity.Rare:
                {
                    return rareMultiplier;
                }
        }
        return 1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("New buff");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
