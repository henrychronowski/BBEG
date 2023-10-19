using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StateTransitionRules", menuName = "ScriptableObjects/Character/State Transition Rules", order = 1)]
[System.Serializable]
public class CharacterStateTransitionRules : ScriptableObject
{
    [System.Flags]
    public enum StateTransitionFlag
    {
        Idle = 1,
        Move = 2,
        Attack = 4,
        Dodge = 8
    }

    public StateTransitionFlag Idle;
    public StateTransitionFlag Move;
    public StateTransitionFlag Attack;
    public StateTransitionFlag Dodge;


    // Use this method to convert the selected options to a readable string
    public string GetSelectedOptionsString(CharacterState cState)
    {
        string stateString = cState.ToString();
        switch(stateString)
        {
            case "Idle":
                {
                    return Idle.ToString();
                }
            case "Move":
                {
                    return Move.ToString();
                }
            case "Attack":
                {
                    return Attack.ToString();
                }
            case "Dodge":
                {
                    return Dodge.ToString();
                }
        }

        // Should never reach here
        return "";
    }

    public List<CharacterState> GetSelectedStates(CharacterState cState)
    {
        string stateString = cState.ToString();
        //Debug.Log("Current State: " + cState.ToString());

        switch (stateString)
        {
            case "Idle":
                {
                    return ParseFlagsForStates(Idle);
                }
            case "Move":
                {
                    return ParseFlagsForStates(Move);
                }
            case "Attack":
                {
                    return ParseFlagsForStates(Attack);
                }
            case "Dodge":
                {
                    return ParseFlagsForStates(Dodge);
                }
        }

        return ParseFlagsForStates(Idle);
    }

    public List<CharacterState> ParseFlagsForStates(StateTransitionFlag options)
    {
        List<CharacterState> states = new List<CharacterState>();
        //Debug.Log("State options: " + options.ToString());
        if(options.ToString().Contains("Idle") || options.ToString() == "-1")
            states.Add(CharacterState.Idle);

        if (options.ToString().Contains("Move") || options.ToString() == "-1")
            states.Add(CharacterState.Move);

        if (options.ToString().Contains("Attack") || options.ToString() == "-1")
            states.Add(CharacterState.Attack);

        if (options.ToString().Contains("Dodge") || options.ToString() == "-1")
            states.Add(CharacterState.Dodge);

        return states;
    }
}
