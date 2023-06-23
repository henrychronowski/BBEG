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
    public Tribe tribe;
    [SerializeField] float followDistance;
    [SerializeField] float brakingDrag; //How much the minion's speed is reduced when in braking range but 
    [SerializeField] float catchupDistance;
    [SerializeField] float catchupSpeedModifier;

    [SerializeField] float mimicFollowDistance;
    [SerializeField] float mimicBrakingDrag;
    [SerializeField] float mimicCatchupDistance;
    [SerializeField] float mimicCatchupSpeedModifier;

    [SerializeField] public bool isStray; // Hasn't joined party yet
    [SerializeField] Rarity rarity;

    

    // Start is called before the first frame update
    void Start()
    {
        state = new IdleState(this);
    }

    private void Update()
    {
        currentState = state.stateType;
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

    public void Stop()
    {
        //if (state.stateType != CharacterState.Idle && state.stateType != CharacterState.Attack)
        //    state = new IdleState(this);
        //else
        rgd.velocity = Vector3.zero;
        axis = Vector2.zero;
    }

    public override void Hit(int damage)
    {
        // Do nothing
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

    private void OnDestroy()
    {
        PlayerCharacterManager.instance.minions.Remove(this);
        Debug.Log(name + " has died");
    }
}
