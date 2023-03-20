using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeTypes
{
    HEALTH,
    SPEED
}
public class UpgradeScript : MonoBehaviour
{
    public PlayerCharacterManager manager;
    int subAmt;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<PlayerCharacterManager>();
    }

    public void Upgrade(int upgrade)
    {
        switch(upgrade)
        {
            case (int)UpgradeTypes.HEALTH:
                subAmt = 10;
                if (manager.tempCurr - subAmt >= 0)
                {
                    manager.leader.health += 10;
                    for (int i = 0; i < manager.minions.Count; i++)
                    {
                        manager.minions[i].health += 10;
                    }
                    manager.tempCurr -= subAmt;
                }
                break;

            case (int)UpgradeTypes.SPEED:
                subAmt = 5;
                if(manager.tempCurr - subAmt >= 0)
                {
                    manager.leader.moveSpeed += 5.0f;
                    for (int i = 0; i < manager.minions.Count; i++)
                    {
                        manager.minions[i].moveSpeed += 5.0f;
                    }
                    manager.tempCurr -= subAmt;
                }
                
                break;
        }
    }
}
