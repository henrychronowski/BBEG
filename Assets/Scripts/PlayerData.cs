using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public Leader leader;
    public List<Minion> minions;
    public PartyMovementState party;
    public int tempCurr;
    public int permCurr;

    public float[] moveSpeed;
    public int[] maxHealth;
    public int[] currHealth;

    public PlayerData(PlayerCharacterManager player)
    {
        party = player.party;

        moveSpeed = new float[player.minions.Count + 1];
        maxHealth = new int[player.minions.Count + 1];
        currHealth = new int[player.minions.Count + 1];

        moveSpeed[0] = player.leader.moveSpeed;
        maxHealth[0] = player.leader.baseHealth;
        currHealth[0] = player.leader.currHealth;
        for (int i = 0; i < player.minions.Count; i++)
        {
            moveSpeed[i + 1] = player.minions[i].moveSpeed;
            maxHealth[i + 1] = player.minions[i].baseHealth;
            currHealth[i + 1] = player.minions[i].currHealth;
        }

        leader = player.leader;
        minions = new List<Minion>(player.minions);

        tempCurr = player.tempCurr;
        permCurr = player.permCurr;
    }
}
