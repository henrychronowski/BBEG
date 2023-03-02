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

    public PlayerData(PlayerCharacterManager player)
    {
        //leader = player.leader;
        party = player.party;

        moveSpeed = new float[player.minions.Count + 1];
        moveSpeed[0] = player.leader.moveSpeed;
        //characterList = new List<Character>(player.characterList);
        for(int i = 0; i < player.minions.Count; i++)
        {
            moveSpeed[i + 1] = player.minions[i].moveSpeed;
        }

        tempCurr = player.tempCurr;
        permCurr = player.permCurr;
    }
}
