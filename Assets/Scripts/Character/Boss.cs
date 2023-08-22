using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

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

    [SerializeField] List<bool> defeatInteractionKeys;

    [SerializeField] YarnProject bossYarnProject;
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
                return "Victory" + victories.ToString();
            }
        }

        if (defeats < defeatInteractionKeys.Count && !wonLastBattle)
        {
            if (victoryInteractionKeys[victories])
            {
                return "Defeat" + victories.ToString();
            }
        }

        return "";
    }

    public void StartEncounter()
    {
        string key = GetInteractionKey();
        encounters++;

        if (key == "" || key == previousInteractionKey)
            return;

        DialogueRunnerEventTranslator.instance.SetProject(bossYarnProject);
        DialogueRunnerEventTranslator.instance.StartDialogue(key);

    }

    void CheckForEncounter(RoomInfo room)
    {
        if(room.gameObject == transform.root.gameObject)
        {
            StartEncounter();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.onRoomEntered += CheckForEncounter;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
