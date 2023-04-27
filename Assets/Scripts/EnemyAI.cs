using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public enum EnemyAIStateType
{
    Wander,
    Aggressive
}


public class EnemyAI : MonoBehaviour
{
    Enemy host; // The enemy that this EnemyAI instance is controlling
    EnemyAIState state;
    [SerializeField] private EnemyAIStateType stateType;
    // Start is called before the first frame update
    void Start()
    {
        host = GetComponent<Enemy>();
        state = new WanderState(host);
        EventManager.instance.onAIStateChanged += ChangeState;
    }

    private void OnDestroy()
    {
        EventManager.instance.onAIStateChanged -= ChangeState;
    }

    void ChangeState(Enemy e, EnemyAIState t)
    {
        if(host == e)
        {
            state = t;
            Debug.Log(transform.name + " has changed to state " + t.stateType.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        state.Update();
        state.TransitionCheck();
        stateType = state.stateType;
    }
}


public abstract class EnemyAIState
{
    public EnemyAIStateType stateType;
    public Enemy host;
    public abstract void Update();

    public abstract void Enter();

    public abstract void TransitionCheck();

}

public class WanderState : EnemyAIState
{
    float timeSpentThisWander;
    float currentWanderTimer;
    Vector2 wanderDirection;
    public WanderState(Enemy enemy)
    {
        host = enemy;
        stateType = EnemyAIStateType.Wander;
        Enter();
    }
    public override void Enter()
    {
        NewWander();
    }

    void NewWander()
    {
        currentWanderTimer = Random.Range(host.minWanderTime, host.maxWanderTime);
        timeSpentThisWander = 0;
        wanderDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    public override void Update()
    {
        host.Move(wanderDirection);
        timeSpentThisWander += Time.deltaTime;
        if(timeSpentThisWander > currentWanderTimer)
        {
            NewWander();
        }

    }

    public override void TransitionCheck()
    {
        //if (Vector3.Distance(PlayerCharacterManager.instance.leader.transform.position, host.transform.position)
        //    <= host.viewRange)
        //{
        //    EventManager.instance.AIStateChanged(host, new AggressiveState(host));
        //}
        if (host.nearbyTargets.Count != 0)
        {
            EventManager.instance.AIStateChanged(host, new AggressiveState(host));
        }
    }
}

public class AggressiveState : EnemyAIState
{
    Character target;
    public AggressiveState(Enemy enemy)
    {
        host = enemy;
        stateType = EnemyAIStateType.Aggressive;
        Enter();
    }
    public override void Enter()
    {

    }

    bool GetTarget()
    {
        target = host.GetClosestTarget();
        return target != null;
    }

    public override void TransitionCheck()
    {
        if(host.nearbyTargets.Count == 0)
        {
            EventManager.instance.AIStateChanged(host, new WanderState(host));
        }
    }

    public override void Update()
    {
        if (!GetTarget())
            return;

        if(Vector3.Distance(host.transform.position, target.transform.position) < host.attackingRange)
        {
            host.AttackStart();
        }
        Vector3 targetDir = (target.transform.position - host.transform.position).normalized;
        host.Move(targetDir);
    }


}