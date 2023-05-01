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
    [SerializeField] protected CharacterState currentState;
    [SerializeField] public int health = 5;
    [SerializeField] public float moveSpeed = 1f;
    [SerializeField] public Rigidbody rgd;
    [SerializeField] public Vector2 axis;
    [SerializeField] public float turnSmoothVelocity;
    [SerializeField] public float turnSmoothTime;
    [SerializeField] public Transform followPoint; // Experimental transform to have minions follow this character
    [SerializeField] public float moveSpeedModifier = 1f;
    [SerializeField] public Attack attack;
    [SerializeField] public Hitbox hitbox;
    [SerializeField] private AnimationOverrider overrider;
    [SerializeField] public Animator animator;

    //[SerializeField] public Image portrait;
    [SerializeField] public Sprite portrait;

    // Contains common functionality between entities (Leader, minions, enemies, NPCs)
    // Movement, attacking, dodging, health, stats etc

    void UpdateOverrider(Attack atk)
    {
        overrider.SetAnimations(atk.animController);
    }
    void UpdateOverrider()
    {
        overrider.SetAnimations(attack.animController);
    }
    void UpdateAnimatorParams()
    {
        if(rgd.velocity != Vector3.zero)
        {
            animator.SetFloat("Blend", 1);
        }
        else if(animator.GetFloat("Blend") > 0)
        {
            animator.SetFloat("Blend", animator.GetFloat("Blend") - 0.1f);
            
        }
        if(animator.GetFloat("Blend") < 0)
        {
            animator.SetFloat("Blend", 0);

        }
    }

    public void Hit(float damage)
    {
        health -= (int)damage; // this will cause problems in damage calc, update playerdata.cs to use float for health
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Move(Vector2 ax, float modifier = 1)
    {
        axis = ax;


        if (state.stateType != CharacterState.Move)
        {
            if (state.stateType == CharacterState.Attack)
                return;
            state = new MoveState(this);
        }

        moveSpeedModifier = modifier;
    }

    public void Move(Vector3 ax, float modifier = 1)
    {
        axis = new Vector2(ax.x, ax.z);

        if (state.stateType != CharacterState.Move)
        {
            if (state.stateType == CharacterState.Attack)
                return;
            state = new MoveState(this);
        }

        moveSpeedModifier = modifier;
    }

    // Useful for when we want to continue keeping track of the char's axis but don't want them to move
    public void UpdateAxis(Vector2 ax)
    {
        axis = ax;

    }

    // Could set to idle state but that causes problems, consider scrapping idle state or adding a transition to 
    public void Stop()
    {
        //if (state.stateType != CharacterState.Idle && state.stateType != CharacterState.Attack)
        //    state = new IdleState(this);
        //else
            rgd.velocity = Vector3.zero;
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
        transform.forward = newDirection;
        //transform.rotation
    }

    public void AttackStart()
    {
        // Play the animation
        if(state.stateType != CharacterState.Attack)
        {
            UpdateOverrider();
            animator.SetTrigger("Attack");
            state = new AttackState(this);
            EventManager.instance.AttackStarted(attack);
        }
    }

    public void AttackStart(Attack atk)
    {
        // Play the animation
        if (state.stateType != CharacterState.Attack)
        {
            UpdateOverrider(atk);
            animator.SetTrigger("Attack");
            state = new AttackState(this, atk);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        state = new IdleState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState = state.stateType;
        state.Update();
    }

    // This is used for the sake of physics, if this becomes a problem later we can add FixedUpdate functions to State
    private void FixedUpdate()
    {
        UpdateAnimatorParams();

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

    // Should only be utilized in timelines, like in cutscenes
    IEnumerator ScriptedMovement(Vector3 newPos)
    {
        float originalDistance = Vector3.Distance(newPos, transform.position);
        while (Vector3.Distance(newPos, transform.position) > PlayerCharacterManager.instance.transitionStoppingDistance)
        {
            Vector3 dir = (newPos - transform.position).normalized;
            float elapsedDistance = Vector3.Distance(newPos, transform.position) / originalDistance;
            Move(new Vector2(dir.x, dir.z), elapsedDistance);
            Debug.Log(Vector3.Distance(newPos, transform.position));
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position, transform.position + rgd.velocity);
        Gizmos.DrawLine(transform.position, transform.position + (transform.forward * 5));

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

    // Entering idle state instantly kills any velocity the character may have had
    public override void Enter()
    {
        c.rgd.velocity = Vector3.zero;
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
        Integrate();
    }

    public void Integrate()
    {
        
        if (c.axis == Vector2.zero)
        {
            //Debug.Log("Moving, Axis = 0 0");

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
        //Integrate();
        
    }
}

public class AttackState : CharacterBaseState
{
    bool canMove;
    float timeElapsed;
    AttackPhase phase;
    Attack attack;
    public AttackState(Character c, bool canMove = false)
    {
        base.c = c;
        stateType = CharacterState.Attack;
        this.canMove = canMove;
        phase = AttackPhase.Startup;
        timeElapsed = 0;
        attack = c.attack;
        Enter();
    }

    public AttackState(Character c, Attack atk, bool canMove = false)
    {
        base.c = c;
        stateType = CharacterState.Attack;
        this.canMove = canMove;
        phase = AttackPhase.Startup;
        attack = atk;
        timeElapsed = 0;
        Enter();
    }

    public override void Enter()
    {
        c.hitbox = attack.GenerateHitbox(c);
        c.hitbox.StartupPhase();
        c.rgd.velocity = Vector2.zero;

    }

    public override void Update()
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed > attack.totalTimeInSeconds)
        {
            c.state = new IdleState(c);
            
            c.Move(c.axis);
            return;
        }

        if(timeElapsed > attack.activeTimeInSeconds + attack.startupInSeconds && phase == AttackPhase.Active)
        {
            c.hitbox.CooldownPhase();
            phase = AttackPhase.Cooldown;
        }

        if (timeElapsed > attack.startupInSeconds && phase == AttackPhase.Startup)
        {
            c.hitbox.ActivePhase();
            phase = AttackPhase.Active;
        }

    }

    public void Integrate()
    {
        if (!canMove)
            return;

        if (c.axis == Vector2.zero)
        {
            Debug.Log("Attacking, Axis = 0 0");
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
