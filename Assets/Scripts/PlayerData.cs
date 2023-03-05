using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public Leader leader;
    public List<Minion> minions;
    public PartyState party;
    public int tempCurr;
    public int permCurr;

    public float[] moveSpeed;
    public int[] health;

    public PlayerData(PlayerCharacterManager player)
    {
        party = player.party;

        moveSpeed = new float[player.minions.Count + 1];
        health = new int[player.minions.Count + 1];

        moveSpeed[0] = player.leader.moveSpeed;
        health[0] = player.leader.health;
        for(int i = 0; i < player.minions.Count; i++)
        {
            moveSpeed[i + 1] = player.minions[i].moveSpeed;
            health[i + 1] = player.minions[i].health;
        }

        leader = player.leader;
        minions = new List<Minion>(player.minions);

        tempCurr = player.tempCurr;
        permCurr = player.permCurr;
    }
}
