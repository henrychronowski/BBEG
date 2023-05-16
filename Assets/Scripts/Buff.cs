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
