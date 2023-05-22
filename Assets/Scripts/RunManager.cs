using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Manages info on the player's current run
// Contains current floor number, tracks win/lose condition
public class RunManager : MonoBehaviour
{
    public int floorNumber = 1;
    [SerializeField] TextMeshProUGUI text;


    void LoadNextFloor()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.onStairsReached += LoadNextFloor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
