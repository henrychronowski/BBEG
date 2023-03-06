using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonGeneration : MonoBehaviour
{
    [SerializeField] Room startingRoom;
    [SerializeField] Room templateRoom; // separate from starting, temporary

    [SerializeField] List<Room> roomSet;
    [SerializeField] float roomGenChance;
    [SerializeField] int currentRooms;
    [SerializeField] int minRooms;
    [SerializeField] int maxRooms;
    [SerializeField] bool useSeed;
    [SerializeField] int seed;
    [SerializeField] Transform spawn;
    [SerializeField] List<Room> generatedRooms;
    
    // How far rooms are placed relative to each other 
    [SerializeField] float roomGapTranslationDistance;
    public void Generate()
    {
        if(useSeed)
            Random.InitState(seed);

        float startingTime = Time.deltaTime;

        SceneManager.LoadScene(startingRoom.name, LoadSceneMode.Additive);

        Room activeRoom = startingRoom;
        generatedRooms = new List<Room>();
        generatedRooms.Add(activeRoom);

        while(currentRooms < maxRooms)
        {
            foreach(Exit ex in activeRoom.GetRoomInfo().exitLocations)
            {
                if(currentRooms == maxRooms)
                {
                    Debug.Log("MAX rooms met, generation complete. Time elapsed: " + (Time.deltaTime - startingTime));

                    return;
                }
                else
                {
                    if(Random.Range(1, 100) < roomGenChance)
                    {
                        currentRooms++;
                        // Load a random room
                        // For now just do templateRoom
                        Room newRoom = templateRoom;
                        newRoom.Load();

                        newRoom.GetRoomInfo().gameObject.transform.Translate(GetTranslationVector(ex), ex.transform);
                        
                    }
                }
            }

            if (currentRooms > minRooms)
            {
                Debug.Log("MIN rooms met, generation complete. Time elapsed: " + (Time.deltaTime - startingTime));
                return;
            }
        }

    }

    public void Clear()
    {
        for(int i = 0; i < roomSet.Count; i++)
        {
            Scene s = SceneManager.GetSceneAt(i);
            if (s.name != "DungeonGenerationTest")
            {
                SceneManager.UnloadSceneAsync(s);
            }
            generatedRooms.Clear();
            currentRooms = 0;
        }
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
