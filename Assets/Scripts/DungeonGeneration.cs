using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class DungeonGeneration : MonoBehaviour
{
    [SerializeField] GameObject startingRoom;
    [SerializeField] Vector2Int startingCoords;

    [SerializeField] RoomSet roomSet;
    [SerializeField] float roomGenChance;
    [SerializeField] int minRooms;
    [SerializeField] int maxRooms;
    [SerializeField] int roomSpawnAttempts;

    // the max amount of times a room can be rerolled when there isn't enough space for it
    [SerializeField] int maxRoomRerolls;

    [SerializeField] bool generateOnPlay;
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
        PlayerCharacterManager.instance.activeRoom = activeRoom;
        CameraControl.Instance.ChangeFocus(activeRoom.focus);

        generatedRooms = new List<RoomInfo>();
        generatedRooms.Add(activeRoom);
        bool reran = false;
        activeRoom.ClearExitConnections();
        
        for(int i = 0; i < generatedRooms.Count; i++)
        {
            activeRoom = generatedRooms[i];

            foreach(Exit ex in activeRoom.exitLocations)
            {
                if(ex.HasConnectingRoom()) // Prevents rooms from being spawned twice
                {
                    roomsAttempted++;
                    continue;
                }
                if(Random.Range(0, 100) < roomGenChance)
                {
                    GameObject instantiatedRoom = null;
                    Collider[] roomCheck;
                    Exit oppositeExit;
                    // Attempts to generate a room until it finds one that fits
                    do
                    {
                        if (instantiatedRoom != null)
                        {
                            Destroy(instantiatedRoom.gameObject);
                            instantiatedRoom = null;
                        }
                        // Pick a random room from a set
                        Room newRoom = roomSet.GetRandomRoom();

                        // Loads in the new room while also adding it to the generatedRooms list
                        instantiatedRoom = Room.LoadRoom(newRoom.roomObj, ex.transform.position + GetTranslationVector(ex));
                        oppositeExit = Room.GetRoomInfo(instantiatedRoom).GetExitsByDirection(Exit.GetOpposingDirection(ex.direction))[0];
                        instantiatedRoom.transform.position -= oppositeExit.transform.localPosition;
                        RoomInfo instantiatedRoomInfo = instantiatedRoom.GetComponent<RoomInfo>();
                        // Check to see if it overlaps other rooms
                        Physics.SyncTransforms();
                        roomCheck = null;
                        roomCheck = Physics.OverlapBox(instantiatedRoomInfo.roomCenter.position, instantiatedRoomInfo.roomExtents / 2, Quaternion.identity, LayerMask.GetMask("RoomExtents"));

                        Debug.Log(roomCheck.Length);

                        if (roomCheck.Length > maxRoomRerolls)
                        {
                            Destroy(instantiatedRoom);
                            instantiatedRoom = null;
                            break;
                        }
                    }
                    while (roomCheck.Length > 1);

                    // Continue condition for when we can't find a room that fits at that exit
                    if (instantiatedRoom == null)
                        continue;

                    // Because the room is being moved after instantiation, we have to
                    // update the camera focus settings to account for the new position
                    instantiatedRoom.GetComponent<CameraFocus>().UpdatePositionFromSettings();

                    generatedRooms.Add(instantiatedRoom.GetComponent<RoomInfo>());
                    // Connect the exits
                    Exit.ConnectExits(ex, oppositeExit);

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

        Exit[] exits = FindObjectsOfType<Exit>();

        foreach(Exit e in exits)
        {
            if (e.connectedExit == null)
                e.gameObject.SetActive(false);
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


    private void OnDrawGizmos()
    {
        
        //Gizmos.color = Color.cyan;
        //for(int i = 0; i <= dungeonGridCellArraySize; i++)
        //{
        //    Gizmos.DrawLine(new Vector3(dungeonGridCellExtents * i, 0, 0),
        //        new Vector3(dungeonGridCellExtents * i, 0, dungeonGridCellExtents * dungeonGridCellArraySize));
        //}
        //for (int j = 0; j <= dungeonGridCellArraySize; j++)
        //{
        //    Gizmos.DrawLine(new Vector3(0, 0, dungeonGridCellExtents * j),
        //               new Vector3(dungeonGridCellExtents * dungeonGridCellArraySize, 0, dungeonGridCellExtents * j));
        //}
        //Gizmos.DrawSphere(GetCenterOfCellWS(14, 0), 5f);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (generateOnPlay)
            Generate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
