using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        c.rgd.velocity = moveDir * (c.GetMoveSpeed() * c.moveSpeedModifier); //?
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
        attack = c.meleeAttack;
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
        if (timeElapsed > attack.totalTimeInSeconds)
        {
            c.state = new IdleState(c);

            c.Move(c.axis);
            return;
        }

        if (timeElapsed > attack.activeTimeInSeconds + attack.startupInSeconds && phase == AttackPhase.Active)
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
    Vector2 dir;
    float timeElapsed;
    public DodgeState(Character c, Vector2 _dir)
    {
        base.c = c;
        stateType = CharacterState.Dodge;
        dir = _dir;
        timeElapsed = 0;
        
        Enter();
    }
    public override void Enter()
    {
        //c.invulnerable = true;
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
        Integrate();

        if (c.dodgeDuration <= c.dodgeInvulDuration)
        {
            c.invulnerable = false;
        }

        if (c.dodgeDuration <= timeElapsed)
        {
            c.invulnerable = false;
            c.transform.forward = dir;
            c.state = new IdleState(c);
        }
    }

    public void Integrate()
    {
        timeElapsed += Time.deltaTime;
        
        float targetAngle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        float angle = Mathf.SmoothDampAngle(c.transform.eulerAngles.y, targetAngle, ref c.turnSmoothVelocity, c.turnSmoothTime);

        c.transform.rotation = Quaternion.Euler(c.transform.eulerAngles.x, c.transform.eulerAngles.y + (360 / (c.dodgeDuration / Time.deltaTime)), c.transform.eulerAngles.z);
        c.rgd.velocity = moveDir * (c.GetMoveSpeed() * c.GetMoveSpeedModifier() * c.dodgeSpeedMultiplier); //?

        
    }
}
