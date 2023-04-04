using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SerializeField]
public enum CharacterState
{
    Actionable,
    Stopped,
    Idle,
    Move,
    Attack,
    Dodge
}

[System.Serializable]
public class Character : MonoBehaviour
{
    [SerializeField] public CharacterBaseState state;
    [SerializeField] public int health = 5;
    [SerializeField] public float moveSpeed = 1f;
    [SerializeField] public Vector3 facing;
    [SerializeField] public Rigidbody rgd;
    [SerializeField] public Vector2 axis;
    [SerializeField] public float turnSmoothVelocity;
    [SerializeField] public float turnSmoothTime;
    [SerializeField] public Transform followPoint; // Experimental transform to have minions follow this character
    [SerializeField] public float moveSpeedModifier = 1f;

    //[SerializeField] public Image portrait;
    [SerializeField] public Sprite portrait;

    // Contains common functionality between entities (Leader, minions, enemies, NPCs)
    // Movement, attacking, dodging, health, stats etc


    public void Move(Vector2 ax, float modifier = 1)
    {
        axis = ax;
        if(state.stateType != CharacterState.Move)
            state = new MoveState(this);

        moveSpeedModifier = modifier;
    }

    public void SetMoveSpeedModifier(float newMod)
    {
        moveSpeedModifier = newMod;
    }

    public float GetMoveSpeedModifier()
    {
        return moveSpeedModifier;
    }

    public void SetFacingDirection(Vector3 newDirection)
    {
        facing = newDirection;
        //transform.rotation
    }

    

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

    // This is used for the sake of physics, if this becomes a problem later we can add FixedUpdate functions to State
    private void FixedUpdate()
    {
        state.FixedUpdate();
    }

    // Contains logic that decides if a state can be switched out of 
    // Basic for now 
    public static bool StateSwitch(Character c, CharacterState s)
    {
        if (c.state.canExit)
        {
            return true;
        }
        else return false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + rgd.velocity);
    }
}

public abstract class CharacterBaseState
{
    public CharacterState stateType;
    public bool canExit = true; // Set to false during certain actions, such as attacks, dodge rolls, or other states that can't be interrupted
    protected Character c;
    public abstract void Update();

    public abstract void FixedUpdate();

    public abstract void Enter();


}

public class IdleState : CharacterBaseState
{
    public IdleState(Character c)
    {
        base.c = c;
        stateType = CharacterState.Idle;
        Enter();
    }

    public override void Enter()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
    }

    

}

public class MoveState : CharacterBaseState
{
    public MoveState(Character c)
    {
        base.c = c;
        stateType = CharacterState.Move;
        Enter();
    }

    

    public override void Enter()
    {
    }

    public override void Update()
    {
        // Unused
    }

    public void Integrate()
    {
        
        if (c.axis == Vector2.zero)
        {
            Debug.Log("Axis = 0 0");
            c.rgd.velocity = Vector2.zero;
            return;
        }
        float targetAngle = Mathf.Atan2(c.axis.x, c.axis.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        float angle = Mathf.SmoothDampAngle(c.transform.eulerAngles.y, targetAngle, ref c.turnSmoothVelocity, c.turnSmoothTime);

        c.transform.rotation = Quaternion.Euler(c.transform.eulerAngles.x, angle, c.transform.eulerAngles.z);
        c.rgd.velocity = moveDir * (c.moveSpeed * c.moveSpeedModifier); //?
    }

    public override void FixedUpdate()
    {
        Integrate();
    }
}

public class AttackState : CharacterBaseState
{
    public AttackState(Character c)
    {
        base.c = c;
        stateType = CharacterState.Attack;
        Enter();
    }
    public override void Enter()
    {
        throw new System.NotImplementedException();
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }
}

public class DodgeState : CharacterBaseState
{
    public DodgeState(Character c)
    {
        base.c = c;
        stateType = CharacterState.Dodge;

        Enter();
    }
    public override void Enter()
    {
        throw new System.NotImplementedException();
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }
}
