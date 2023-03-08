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
    [SerializeField] float brakingDrag; //How much the minion's speed is reduced when in braking range but 
    [SerializeField] float catchupDistance;
    [SerializeField] float catchupSpeedModifier;

    [SerializeField] float mimicFollowDistance;
    [SerializeField] float mimicBrakingDrag;
    [SerializeField] float mimicCatchupDistance;
    [SerializeField] float mimicCatchupSpeedModifier;


    // Start is called before the first frame update
    void Start()
    {
        state = new IdleState(this);
        leader = FindObjectOfType<Leader>();
    }

    // Update is called once per frame
    void Update()
    {
        state.Update();
    }

    // Forcibly set the minion state to move no matter what they're doing
    public void Follow(Transform point)
    {
        if(Vector3.Distance(transform.position, point.position) > followDistance)
        {
            if (state.stateType != CharacterState.Move)
                state = new MoveState(this);
            Vector3 dir = (point.position - transform.position).normalized;
            Move(new Vector2(dir.x, dir.z));
        }
        else
        {
            if (state.stateType != CharacterState.Move)
                state = new MoveState(this);
            Vector3 dir = (point.position - transform.position).normalized;
            Move(new Vector2(dir.x, dir.z));
        }
        //else if (Vector3.Distance(transform.position, point.position) > stoppingDistance)
        //{
        //    moveSpeedModifier *= brakingDrag;
        //    if (state.stateType != CharacterState.Move)
        //        state = new MoveState(this);
        //    Vector3 dir = (point.position - transform.position).normalized;
        //    Move(new Vector2(dir.x, dir.z));
        //}
        //else
        //{
        //    moveSpeedModifier = 1;
        //    if (state.stateType != CharacterState.Idle)
        //        state = new IdleState(this);
        //    Move(Vector2.zero);
        //}
    }

    // Called when Leader stops moving during Follow party state
    public void Stop()
    {
        if (state.stateType != CharacterState.Idle)
                state = new IdleState(this);
        
        Move(Vector2.zero);
    }

    public void Mimic(Transform point)
    {
        if (Vector3.Distance(transform.position, point.position) > mimicCatchupDistance)
        {
            moveSpeedModifier = mimicCatchupSpeedModifier;
            if (state.stateType != CharacterState.Move)
                state = new MoveState(this);
            Vector3 dir = (point.position - transform.position).normalized;
            Move(new Vector2(dir.x, dir.z), mimicCatchupSpeedModifier);
            Debug.Log(name + " catching up");
        }
        else if (Vector3.Distance(transform.position, point.position) > mimicFollowDistance)
        {
            if (state.stateType != CharacterState.Move)
                state = new MoveState(this);
            Vector3 dir = (point.position - transform.position).normalized;
            Move(new Vector2(dir.x, dir.z), 1.25f);
            Debug.Log(name + " lagging");

        }
        else
        {
            moveSpeedModifier = 1f;
            if (state.stateType != CharacterState.Move)
                state = new MoveState(this);
            transform.position = point.position;
            Vector3 dir = leader.axis;
            Move(new Vector2(dir.x, dir.z), 1.25f);
            Debug.Log(name + " synced moving towards " + dir);
        }
    }

    public void NewMimic(Transform point)
    {
        if (Vector3.Distance(transform.position, point.position) > mimicFollowDistance)
        {
            if (state.stateType != CharacterState.Move)
                state = new MoveState(this);
            Vector3 dir = (point.position - transform.position).normalized;
            Move(new Vector2(dir.x, dir.z), mimicCatchupSpeedModifier);
            Debug.Log(name + " lagging");

        }
        else
        {
            transform.position = point.position;
        }
    }
}
