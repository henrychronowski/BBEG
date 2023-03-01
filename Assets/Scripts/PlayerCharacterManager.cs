using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public enum PartyState
{
    Follow, // Inputs only get sent to the Leader
    Mimic // Inputs get sent to whole party
}

public enum MimicFormations // Formations MUST be organized in this order in mimicPointParents or else the wrong formations will be used
{ 
    North,
    East,
    South,
    West
}

public class PlayerCharacterManager : MonoBehaviour
{
    // Takes input and sends it to the proper Character(s)
    // 
    [SerializeField] int maxCharacters = 4;
    [SerializeField] public List<Minion> minions;
    [SerializeField] public Leader leader;
    [SerializeField] public PartyState party;
    // Combined list of minions + leader
    [SerializeField] public List<Character> characterList;

    [SerializeField] Transform mimicPointsContainer;
    [SerializeField] public Transform currentMimicPointsParent;
    [SerializeField] public List<Transform> mimicPointParents;

    //Temporary currency (used only during runs) and
    //Permanent currency (used throughout the game)
    [SerializeField] public int tempCurr;
    [SerializeField] public int permCurr;

    public Text txt1;
    public Text txt2;

    private void OnMove(InputValue val)
    {
        Vector2 axis = Vector2.zero;

        if(val.Get() != null)
            axis = (Vector2)val.Get();

        switch(party)
        {
            case PartyState.Follow:
                {
                    leader.Move(axis);
                    //for(int i = 0; i < minions.Count; i++)
                    //{
                    //    // Minion 0 follows leader, minion 1 follows minion 0, etc
                    //    if (i == 0)
                    //    {
                    //        minions[i].Follow(leader);
                    //        continue;
                    //    }
                    //    minions[i].Follow(minions[i-1]);
                    //}
                    break;
                }
            case PartyState.Mimic:
                {
                    leader.Move(axis);

                    
                    break;
                }
        }
    }

    private void OnMimicStart(InputValue val)
    {
        Vector2 axis = Vector2.zero;
        if (val.Get() != null)
            axis = (Vector2)val.Get();

        if (axis == Vector2.zero)
            return;

        party = PartyState.Mimic;

        // It's currently possible for diagonal inputs to register here, which would prioritize the X axis
        // Will fix later

        switch (axis.x)
        {
            case -1: // West
                {
                    currentMimicPointsParent = mimicPointParents[(int)MimicFormations.West];

                    return;
                }
            case 1: // East
                {
                    currentMimicPointsParent = mimicPointParents[(int)MimicFormations.East];
                    return;
                }
        }
        switch (axis.y)
        {
            case -1: // South
                {
                    currentMimicPointsParent = mimicPointParents[(int)MimicFormations.South];

                    return;
                }
            case 1: // North
                {
                    currentMimicPointsParent = mimicPointParents[(int)MimicFormations.North];

                    return;
                }
        }


    }

    public void OnMimicEnd()
    {
        party = PartyState.Follow;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mimicPointsContainer.position = leader.transform.position;


        switch (party)
        {
            case PartyState.Follow:
                {
                    if (leader.axis == Vector2.zero)
                    {
                        for (int i = 0; i < minions.Count; i++)
                        {
                            minions[i].Stop();
                        }
                        break;
                    }

                    for (int i = 0; i < minions.Count; i++)
                    {
                        // Minion 0 follows leader, minion 1 follows minion 0, etc
                        if (i == 0)
                        {
                            minions[i].Follow(leader.followPoint);
                            continue;
                        }
                        minions[i].Follow(minions[i - 1].followPoint);
                    }
                    break;
                }
            case PartyState.Mimic:
                {
                    // If a minion is too far away it will try to move back towards its point in Mimic
                    for (int i = 0; i < minions.Count; i++)
                    {
                        minions[i].Move(leader.axis);
                        minions[i].NewMimic(currentMimicPointsParent.GetChild(i));
                    }

                    break;
                }
        }

        txt1.text = tempCurr.ToString();
        txt2.text = permCurr.ToString();
    }

    public void LoadData(PlayerData data)
    {
        leader = data.leader;
        party = data.party;

        //characterList = new List<Character>(data.characterList);
        for (int i = 0; i < characterList.Count; i++)
        {
            characterList[i].moveSpeed = data.moveSpeed[i];
        }

        tempCurr = data.tempCurr;
        permCurr = data.permCurr;
    }
}
