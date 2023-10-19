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
    [SerializeField] bool roomEntered;
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
        roomEntered = true;
        string key = GetInteractionKey();
        encounters++;

        if (key == "" || key == previousInteractionKey)
        {
            StartFightCheck(true);
            return;
        }

        DialogueRunnerEventTranslator.instance.SetProject(bossYarnProject);
        DialogueRunnerEventTranslator.instance.StartDialogue(key);
        RoomInfo roomInfo = transform.parent.GetComponentInParent<RoomInfo>();
        roomInfo.FreezeEnemies();
    }

    void CheckForEncounter(RoomInfo room)
    {
        if(room.gameObject == transform.root.gameObject)
        {
            StartEncounter();
        }
    }

    void StartFightCheck()
    {
        if(roomEntered)
            transform.parent.GetComponentInParent<RoomInfo>().UnfreezeEnemies();

    }

    void StartFightCheck(bool forceStart = false)
    {
        if (roomEntered || forceStart)
            transform.parent.GetComponentInParent<RoomInfo>().UnfreezeEnemies();

    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.onRoomEntered += CheckForEncounter;
        EventManager.instance.onDialogueComplete += StartFightCheck;
    }
    private void OnDestroy()
    {
        EventManager.instance.onRoomEntered -= CheckForEncounter;
        EventManager.instance.onDialogueComplete -= StartFightCheck;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
