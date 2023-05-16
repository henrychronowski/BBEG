using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public enum PartyMovementState
{
    Follow, // Inputs only get sent to the Leader
    Mimic, // Inputs get sent to whole party
    Scripted // Inputs are being handled by a script instead of the player, used in room transitions/cutscenes
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

    public static PlayerCharacterManager instance;
    [SerializeField] int maxMinions = 3;
    [SerializeField] public List<Minion> minions;
    [SerializeField] public Leader leader;
    [SerializeField] public PartyMovementState party;
    [SerializeField] bool attacking;

    [SerializeField] protected Transform mimicPointsContainer;
    [SerializeField] public Transform currentMimicPointsParent;
    [SerializeField] public List<Transform> mimicPointParents;

    [SerializeField] public float transitionStoppingDistance;
    [SerializeField] public float transitionSpeedModifier;

    // How far in front of the player the minions will be when attacking
    [SerializeField] float minionAttackDisplacement;

    // The index of the character that will attack next in the combo
    [SerializeField] int currentAttackIndex;

    [SerializeField] PlayerInput input;
    public RoomInfo activeRoom;

    //Temporary currency (used only during runs) and
    //Permanent currency (used throughout the game)
    [SerializeField] public int tempCurr;
    [SerializeField] public int permCurr;

    public TextMeshProUGUI stateView;

    private void OnMove(InputValue val)
    {
        Vector2 axis = Vector2.zero;

        if (val.Get() != null)
            axis = (Vector2)val.Get();

        // Lock the player out of moving when anyone is attacking
        if (attacking)
        {
            //leader.UpdateAxis(axis);
            leader.Move(axis, 0);
            return;
        }

        switch (party)
        {
            case PartyMovementState.Follow:
                {
                    leader.Move(axis);

                    break;
                }
            case PartyMovementState.Mimic:
                {
                    leader.Move(axis);


                    break;
                }
        }
    }

    // Move

    public bool AddMinion(Minion newMinion)
    {
        if(minions.Count == maxMinions)
        {
            return false;
        }

        minions.Add(newMinion);
        EventManager.instance.NewMinionAdded(newMinion);
        return true;
    }

    private void OnLightAttack()
    {
        if (party == PartyMovementState.Scripted)
            return;

        if (attacking && currentAttackIndex < minions.Count)
        {
            //leader.AttackStart(minions[currentAttackIndex].attack);
            minions[currentAttackIndex].SetFacingDirection(leader.transform.forward);
            minions[currentAttackIndex].transform.position = leader.transform.position + (leader.transform.forward * minionAttackDisplacement) + ((leader.transform.right * currentAttackIndex) - leader.transform.right);
            
            minions[currentAttackIndex].AttackStart();
            currentAttackIndex++;
        }
        else
        {
            leader.AttackStart();
            StopMinions();
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
        CharacterState[] states = new CharacterState[minions.Count + 1];
        states[0] = leader.state.stateType;

        for (int i = 0; i < minions.Count; i++)
        {
            states[i + 1] = minions[i].state.stateType;
        }

        return states;
    }


    bool IsCharacterInPartyInState(CharacterState desiredState)
    {
        CharacterState[] states = GetPartyCharacterStates();

        foreach (CharacterState s in states)
        {
            if (s == desiredState)
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
        if (attacking)
        {
            leader.Move(leader.axis, 0);
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
                        minions[i].transform.forward = leader.transform.forward;
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

    // Stops all minions
    void StopMinions()
    {
        for (int i = 0; i < minions.Count; i++)
        {
            minions[i].Stop();
        }
    }

    IEnumerator RoomTransition(Vector3 newPos)
    {
        party = PartyMovementState.Scripted;
        float originalDistance = Vector3.Distance(newPos, leader.transform.position);
        while (Vector3.Distance(newPos, leader.transform.position) > transitionStoppingDistance)
        {
            Vector3 dir = (newPos - leader.transform.position).normalized;
            float elapsedDistance = Vector3.Distance(newPos, leader.transform.position) / originalDistance;
            leader.Move(new Vector2(dir.x, dir.z));
            Debug.Log(Vector3.Distance(newPos, leader.transform.position));
            yield return null;

        }
        //leader.transform.position = new Vector3(newPos.x, leader.transform.position.y, newPos.z);

        // OnMove() only gets called when the movement input axis changes
        // Without this line, if the player holds a direction during the scripted movement their axis never gets updated
        // properly since it hasn't changed since the scripted movement ended
        leader.SetMoveSpeedModifier(1);
        leader.axis = input.currentActionMap.FindAction("Move").ReadValue<Vector2>();
        party = PartyMovementState.Mimic;
        
        
    }


    public void StartTransition(Vector3 newPos)
    {
        StartCoroutine(RoomTransition(newPos));
    }

    protected void FollowUpdate()
    {
        if (leader.axis == Vector2.zero || leader.rgd.velocity == Vector3.zero)
        {
            for (int i = 0; i < minions.Count; i++)
            {
                minions[i].Stop();
            }
            return;
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
    }

    void ScriptedUpdate()
    {
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
    }

    public static bool HitboxAllegianceCheck(GameObject hitboxOwner, GameObject recipient)
    {
        if((hitboxOwner.tag == "Player" || hitboxOwner.tag == "Minion") && recipient.tag == "Enemy")
        {
            return true;
        }

        if (hitboxOwner.tag == "Enemy" && (recipient.tag == "Player" || recipient.tag == "Minion"))
        {
            return true;
        }

        

        return false;
    }

    static void HitCharacter(HitData data)
    {
        data.mRecipient.Hit(data.ProcessDamage());
    }

    

    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.onHitProcessed += HitCharacter;
    }

    private void OnDestroy()
    {
        EventManager.instance.onHitProcessed -= HitCharacter;
    }

    private void Update()
    {
        attacking = IsCharacterInPartyInState(CharacterState.Attack);
        PartyStateUpdate();
        if(party == PartyMovementState.Follow)
        {
            stateView.text = "Party State: " + party.ToString() + "\nFormation: None";

        }
        else
        {
            stateView.text = "Party State: " + party.ToString() + "\nFormation: " + currentMimicPointsParent.name;

        }
    }

    public void LoadData(PlayerData data)
    {
        //leader = data.leader;
        party = data.party;

        leader.moveSpeed = data.moveSpeed[0];
        leader.baseHealth = data.maxHealth[0];
        leader.currHealth = data.currHealth[0];
        //characterList = new List<Character>(data.characterList);
        for (int i = 0; i < minions.Count; i++)
        {
            minions[i].moveSpeed = data.moveSpeed[i + 1];
            minions[i].baseHealth = data.maxHealth[i + 1];
            minions[i].currHealth = data.currHealth[i + 1];
        }

        leader = data.leader;
        minions = new List<Minion>(data.minions);

        tempCurr = data.tempCurr;
        permCurr = data.permCurr;
    }
    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        else
            instance = this;
    }

}
