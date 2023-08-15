using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // These variables will ideally be accessed from save data down the line, using these for now
    
    enum LastBattleResult
    {
        Victory,
        Defeat
    }

    [SerializeField] int encounters;
    [SerializeField] int victories;
    [SerializeField] int defeats;

    // used to avoid playing the same interaction twice in a row
    [SerializeField] string previousInteractionKey;
    [SerializeField] bool wonLastBattle;

    // used to determine if the current victory count has an interaction
    [SerializeField] List<bool> victoryInteractionKeys;
    public string GetInteractionKey()
    {
        if(encounters == 0)
        {
            return "FirstMeeting";
        }

        if(victories < victoryInteractionKeys.Count && wonLastBattle)
        {
            if (victoryInteractionKeys[victories])
            {

            }
        }        

        return "Error";
    }

    public void StartEncounter()
    {
        string key = GetInteractionKey();
        encounters++; 
        
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
