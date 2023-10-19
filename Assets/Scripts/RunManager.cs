using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Area
{
    Forest,
    City,
    Castle
}

// Manages info on the player's current run
// Contains current floor number, tracks win/lose condition
public class RunManager : MonoBehaviour
{
    public int floorNumber = 1;
    public int demoGoalFloor = 10;
    public Area currentArea;
    [SerializeField] TextMeshProUGUI text;


    void IncrementFloorNumber()
    {
        floorNumber++;
        text.text = currentArea.ToString() + "\nFloor " + floorNumber.ToString();
        if(floorNumber >= demoGoalFloor)
        {
            EventManager.instance.DemoEndReached();
        }
    }

    private void OnMove(InputValue val)
    {
        Debug.Log("A");
    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.onStairsReached += IncrementFloorNumber;
    }

    private void OnDestroy()
    {
        EventManager.instance.onStairsReached -= IncrementFloorNumber;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
