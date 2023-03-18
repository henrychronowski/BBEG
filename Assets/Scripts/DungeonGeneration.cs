using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class DungeonGeneration : MonoBehaviour
{
    [SerializeField] GameObject startingRoom;

    [SerializeField] RoomSet roomSet;
    [SerializeField] float roomGenChance;
    [SerializeField] int minRooms;
    [SerializeField] int maxRooms;
    [SerializeField] int roomSpawnAttempts;

    [SerializeField] bool useSeed;
    [SerializeField] int seed;
    [SerializeField] Transform spawn;
    [SerializeField] List<RoomInfo> generatedRooms;
    
    // How far rooms are placed relative to each other 
    [SerializeField] float roomGapTranslationDistance;
    public void Generate()
    {
        Clear();

        if(useSeed)
            Random.InitState(seed);

        float startingTime = Time.deltaTime;
        int roomsAttempted = 0;

        RoomInfo activeRoom = Room.LoadRoom(startingRoom, Vector3.zero).GetComponent<RoomInfo>();
        
        generatedRooms = new List<RoomInfo>();
        generatedRooms.Add(activeRoom);
        bool reran = false;
        activeRoom.ClearExitConnections();
        //while(currentRooms < maxRooms)
        
        for(int i = 0; i < generatedRooms.Count; i++)
        {
            activeRoom = generatedRooms[i];

            foreach(Exit ex in activeRoom.exitLocations)
            {
                if(ex.HasConnectingRoom()) // Prevents duplicates
                {
                    roomsAttempted++;
                    continue;
                }
                if(Random.Range(1, 100) < roomGenChance)
                {
                    // Pick a random room from a set
                    Room newRoom = roomSet.GetRandomRoom();

                    // Loads in the new room while also adding it to the generatedRooms list
                    GameObject instantiatedRoom = Room.LoadRoom(newRoom.roomObj, activeRoom.transform.position + GetTranslationVector(ex));
                    generatedRooms.Add(instantiatedRoom.GetComponent<RoomInfo>());

                    // Connect the exits
                    Exit.ConnectExits(ex, Room.GetRoomInfo(instantiatedRoom).GetExitsByDirection(Exit.GetOpposingDirection(ex.direction))[0]);

                    // Connect the rooms? Might be unnecessary but keeping it for now
                    ex.AddConnectingRoom(Room.GetRoomInfo(instantiatedRoom));
                    Room.GetRoomInfo(instantiatedRoom).GetExitsByDirection(Exit.GetOpposingDirection(ex.direction))[0].AddConnectingRoom(activeRoom);

                    // ID = order in which it was generated
                    instantiatedRoom.gameObject.name = "ID: " + generatedRooms.Count.ToString();
                }
                
                roomsAttempted++;
                // Breaks out of this foreach, which will then trigger the exit condition
                if(generatedRooms.Count == maxRooms)
                {
                    break;
                }
                
                
            }

            // Exit condition
            if (roomsAttempted > roomSpawnAttempts || generatedRooms.Count >= maxRooms)
            {
                break;
            }

            // Sets i to -1 and calls continue, effectively restarting the for loop
            // I don't like it but it's 1:52am
            if(i+1 == generatedRooms.Count && generatedRooms.Count < minRooms && !reran)
            {
                i = -1;
                //reran = true;
                continue;
            }
            
        }
        Debug.Log("Generation complete, roomsAttempted = " + roomsAttempted);

    }

    
    public void Clear()
    {
        for(int i = 0; i < generatedRooms.Count; i++)
        {
            Destroy(generatedRooms[i].gameObject);
        }
            generatedRooms.Clear();
    }

    Vector3 GetTranslationVector(Exit ex)
    {
        switch (ex.direction)
        {
            case ExitDirection.North:
                {

                    return Vector3.forward * roomGapTranslationDistance;
                }
            case ExitDirection.East:
                {

                    return Vector3.right * roomGapTranslationDistance;
                }
            case ExitDirection.South:
                {

                    return Vector3.back * roomGapTranslationDistance;
                }
            case ExitDirection.West:
                {

                    return Vector3.left * roomGapTranslationDistance;
                }
        }
        return Vector3.zero;
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
