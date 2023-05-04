using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInfo : MonoBehaviour
{
    // Contains specific data about rooms, such as exit locations
    // Does not contain properties such as a room's rarity or type, that stays in Room.cs
    // Attached to each RoomObj, contains variables that need to be accessed during gameplay
    public List<Exit> exitLocations;
    public CameraFocus focus;

    public List<Exit> GetExitsByDirection(ExitDirection dir)
    {
        List<Exit> exits = new List<Exit>();
        
        foreach(Exit e in exitLocations)
        {
            if(e.direction == dir)
                exits.Add(e);
        }

        if (exits.Count == 0)
        {
            Debug.Log("No exits found that match direction, returning exit list of size 1 containing exit 0");
            exits.Add(exitLocations[0]);
        }

        return exits;
    }

    // Purely for debugging purposes
    public void ClearExitConnections()
    {
        foreach(Exit e in exitLocations)
        {
            e.connectedRoom = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
