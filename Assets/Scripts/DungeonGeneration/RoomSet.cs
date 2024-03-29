using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRoomSet", menuName = "ScriptableObjects/Room Set", order = 1)]
[System.Serializable]
public class RoomSet : ScriptableObject
{
    // RoomSets are given to DungeonGeneration upon loading into a new level (forest, castle etc)
    // Each room can be given its own rarity via the RoomSet editor

    public GameObject[] roomArray;
    public int[] roomWeight;
    public int count;
    public GameObject exitRoom;

    public void Resize(int newSize)
    {
        count = newSize;
        System.Array.Resize(ref roomArray, newSize);
        System.Array.Resize(ref roomWeight, newSize);
    }

    public GameObject GetRandomRoom()
    {
        int totalWeight = 0;

        foreach (int w in roomWeight)
            totalWeight += w;

        int random = Random.Range(1, totalWeight+1);

        int currentWeight = 0;

        for(int i = 0; i < roomWeight.Length; i++)
        {
            currentWeight += roomWeight[i];
            if(random <= currentWeight)
            {
                //Debug.Log("Generating Room " + roomArray[i].name + " of weight " + roomWeight[i].ToString() + ". Random = " + random.ToString());
                return roomArray[i];
            }
        }
        Debug.Log("Failsafe returning 0");

        return roomArray[0];
    }

}
