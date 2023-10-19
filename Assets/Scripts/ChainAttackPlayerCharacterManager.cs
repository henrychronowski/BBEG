using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ChainAttackPlayerCharacterManager : PlayerCharacterManager
{
    [SerializeField] int currentAttackIndex;
    public static new ChainAttackPlayerCharacterManager instance;
    

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        else
            instance = this;
    }

    private void OnMove(InputValue val)
    {
        Vector2 axis = Vector2.zero;

        if (val.Get() != null)
            axis = (Vector2)val.Get();

        switch (party)
        {
            case PartyMovementState.Follow:
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
            case PartyMovementState.Mimic:
                {
                    leader.Move(axis);


                    break;
                }
        }
    }

    private void OnLightAttack()
    {
        //leader.Move(Vector2.zero);
        if(leader.state.stateType == CharacterState.Attack && currentAttackIndex < minions.Count)
        {
            //leader.AttackStart(minions[currentAttackIndex].attack);
            minions[currentAttackIndex].AttackStart();
            currentAttackIndex++;
        }
        else
        {
            leader.AttackStart();
            currentAttackIndex = 0;
        }
    }
    private void OnMimicStart(InputValue val)
    {
        Vector2 axis = Vector2.zero;
        if (val.Get() != null)
            axis = (Vector2)val.Get();

        if (axis == Vector2.zero)
            return;

        party = PartyMovementState.Mimic;

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
        party = PartyMovementState.Follow;
    }

    CharacterState[] GetPartyCharacterStates()
    {
        CharacterState[] states = new CharacterState[minions.Count+1];
        states[0] = leader.state.stateType;

        for(int i = 0; i < minions.Count; i++)
        {
            states[i + 1] = minions[i].state.stateType;
        }

        return states;
    }

    
    bool IsCharacterInPartyInState(CharacterState desiredState)
    {
        CharacterState[] states = GetPartyCharacterStates();

        foreach(CharacterState s in states)
        {
            if(s == desiredState)
            {
                return true;
            }
        }
        return false;
    }

    void PartyStateUpdate()
    {
        mimicPointsContainer.position = leader.transform.position;

        // Don't run this if anyone is in an attacking state
        if(IsCharacterInPartyInState(CharacterState.Attack))
        {
            return;
        }

        switch (party)
        {
            case PartyMovementState.Follow:
                {
                    FollowUpdate();
                    break;
                }
            case PartyMovementState.Mimic:
                {
                    // If a minion is too far away it will try to move back towards its point in Mimic
                    for (int i = 0; i < minions.Count; i++)
                    {
                        minions[i].Move(leader.axis);
                        minions[i].NewMimic(currentMimicPointsParent.GetChild(i));
                    }

                    break;
                }
            case PartyMovementState.Scripted: // 
                {
                    FollowUpdate();
                    break;
                }
        }
    }
    private void Update()
    {
        PartyStateUpdate();
    }

}
