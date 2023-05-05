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
    [SerializeField] public CharacterState currentState;
    [SerializeField] public List<Buff> activeBuffs;

    [SerializeField] public int baseHealth = 5;
    [SerializeField] public int currHealth = 5;
    [SerializeField] public float moveSpeed = 1f;
    [SerializeField] public Rigidbody rgd;
    [SerializeField] public Vector2 axis;
    [SerializeField] public float turnSmoothVelocity;
    [SerializeField] public float turnSmoothTime;
    [SerializeField] public Transform followPoint; // Experimental transform to have minions follow this character
    [SerializeField] public float moveSpeedModifier = 1f;

    [SerializeField] public Attack meleeAttack;
    [SerializeField] public Attack rangedAttack;
    [SerializeField] public int baseMeleeAffinity;
    [SerializeField] public int baseRangedAffinity;

    [SerializeField] public int baseDefense;

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
        overrider.SetAnimations(meleeAttack.animController);
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
        currHealth -= (int)meleeAttack.damage; // this will cause problems in damage calc, update playerdata.cs to use float for health
        if(currHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public virtual ArtifactTarget GetCharacterType()
    {
        return 0;
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
            EventManager.instance.AttackStarted(meleeAttack);
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

