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
    [SerializeField] public int baseDefense = 0;
    [SerializeField] public float moveSpeed = 1f;
    [SerializeField] public int baseMeleeAffinity;
    [SerializeField] public int baseRangedAffinity;
    [SerializeField] public Rigidbody rgd;
    [SerializeField] public Vector2 axis;
    [SerializeField] public float turnSmoothVelocity;
    [SerializeField] public float turnSmoothTime;
    [SerializeField] public Transform followPoint; // Experimental transform to have minions follow this character
    [SerializeField] public float moveSpeedModifier = 1f; // Applies a modifier to moveSpeed, useful for terrain that slows

    [SerializeField] public Attack meleeAttack;
    [SerializeField] public Attack rangedAttack;


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

    public void Hit(int damage)
    {
        if(damage < 0)
            damage = 0;
        currHealth -= damage; 
        
        if(currHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Heal(int healValue)
    {
        currHealth += healValue;
        if(currHealth > GetMaxHealth())
        {
            currHealth = GetMaxHealth();
        }
    }

    public float GetMoveSpeed()
    {
        float moveSpeedBuffTotal = 0;
        foreach (Buff b in activeBuffs)
        {
            moveSpeedBuffTotal += b.movementSpeedBuff;
        }
        return moveSpeedBuffTotal + moveSpeed;
    }

    public int GetMaxHealth()
    {
        int maxHealthBuffTotal = 0;
        foreach(Buff b in activeBuffs)
        {
            maxHealthBuffTotal += b.maxHealthBuff;
        }
        return maxHealthBuffTotal + baseHealth;
    }

    public int GetDefense()
    {
        int defenseBuffTotal = 0;
        foreach (Buff b in activeBuffs)
        {
            defenseBuffTotal += b.defenseBuff;
        }
        return defenseBuffTotal;
    }

    public int GetMeleeAffinity()
    {
        int meleeAffinityBuffTotal = 0;
        foreach (Buff b in activeBuffs)
        {
            meleeAffinityBuffTotal += b.meleeAttackBuff;
        }
        return meleeAffinityBuffTotal + baseMeleeAffinity;
    }

    public int GetRangedAffinity()
    {
        int rangedAffinityBuffTotal = 0;
        foreach (Buff b in activeBuffs)
        {
            rangedAffinityBuffTotal += b.rangedAttackBuff;
        }
        return rangedAffinityBuffTotal + baseRangedAffinity;
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

