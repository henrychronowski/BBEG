using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public Leader leader;
    public List<Character> characterList;
    public PartyState party;
    public int tempCurr;
    public int permCurr;

    public PlayerData(PlayerCharacterManager player)
    {
        leader = player.leader;
        party = player.party;

        /*characterList = new List<Character>();
        for(int i = 0; i < player.characterList.Count; i++)
        {
            characterList[i] = player.characterList[i];
        }*/

        tempCurr = player.tempCurr;
        permCurr = player.permCurr;
    }
}
