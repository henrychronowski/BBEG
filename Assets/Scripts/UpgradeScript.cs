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
                break;

            case (int)UpgradeTypes.SPEED:
                for(int i = 0; i < manager.characterList.Count; i++)
                {
                    manager.characterList[i].moveSpeed += 5.0f;
                }
                manager.tempCurr -= 5;
                break;
        }
    }
}
