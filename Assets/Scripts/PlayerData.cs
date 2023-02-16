using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public Leader leader;
    public Minion[] minions;

    public PlayerData(PlayerCharacterManager player)
    {
        leader = player.leader;

        minions = new Minion[player.minions.Count];
        for(int i = 0; i < player.minions.Count; i++)
        {
            minions[i] = player.minions[i];
        }
    }
}
