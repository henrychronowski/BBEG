using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


enum PartyState
{
    Follow, // Inputs only get sent to the Leader
    Mimic // Inputs get sent to whole party
}
public class PlayerCharacterManager : MonoBehaviour
{
    // Takes input and sends it to the proper Character(s)
    // 
    [SerializeField] int maxCharacters = 4;
    [SerializeField] List<Minion> minions;
    [SerializeField] Leader leader;
    [SerializeField] PartyState party;
    // Combined list of minions + leader
    [SerializeField] List<Character> characterList;

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
                    foreach(Character character in characterList)
                    {
                        character.Move(axis);
                    }
                    break;
                }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (party)
        {
            case PartyState.Follow:
                {

                    for (int i = 0; i < minions.Count; i++)
                    {
                        // Minion 0 follows leader, minion 1 follows minion 0, etc
                        if (i == 0)
                        {
                            minions[i].Follow(leader);
                            continue;
                        }
                        minions[i].Follow(minions[i - 1]);
                    }
                    break;
                }
            //case PartyState.Mimic:
            //    {
            //        foreach (Character character in characterList)
            //        {
            //            character.Move(axis);
            //        }
            //        break;
            //    }
        }
    }
}
