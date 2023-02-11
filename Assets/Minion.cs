using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Minion : Character
{
    // Checks partyState from PCM (PlayerCharacterManager.cs) every frame to determine action to take
    // Follow:
    // 1) Set hurtbox to be intangible from harm
    // 2) Ask PCM to return the Character it should be following and set it as the target (Could be the Leader or another Minion)
    // 3) Move in the direction of the target
    // 


    // Mimic:
    // 1) Execute actions through Character functions
    // 2) Have some kind of spring force that keeps the formation?


    // Contains a trait?

    [SerializeField] Leader leader;
    [SerializeField] float followDistance;
    [SerializeField] float stoppingDistance;

    // Start is called before the first frame update
    void Start()
    {
        state = new IdleState(this);

    }

    // Update is called once per frame
    void Update()
    {
        state.Update();
    }

    // Forcibly set the minion state to move no matter what they're doing
    public void Follow(Character character)
    {
        if (Vector3.Distance(transform.position, character.transform.position) > followDistance)
        {
            if (state.stateType != CharacterState.Move)
                state = new MoveState(this);
            Vector3 dir = (character.transform.position - transform.position).normalized;
            Move(new Vector2(dir.x, dir.z));
        }
        else
        {
            if (state.stateType != CharacterState.Idle)
                state = new IdleState(this);
            Move(Vector2.zero);
        }
    }
}
