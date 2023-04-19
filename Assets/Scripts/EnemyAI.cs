using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyAIStateType
{
    Wander,
    Aggressive
}


public class EnemyAI : MonoBehaviour
{
    EnemyAIState state;
     Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        state = new WanderState(enemy);

    }

    // Update is called once per frame
    void Update()
    {
        state.Update();
    }
}


public abstract class EnemyAIState
{
    public EnemyAIStateType stateType;
    public Enemy e;
    public abstract void Update();

    public abstract void Enter();

}

public class WanderState : EnemyAIState
{
    float timeSpentThisWander;
    float currentWanderTimer;
    Vector2 wanderDirection;
    public WanderState(Enemy enemy)
    {
        e = enemy;
        stateType = EnemyAIStateType.Wander;
        Enter();
    }
    public override void Enter()
    {
        NewWander();
    }

    void NewWander()
    {
        currentWanderTimer = Random.Range(e.minWanderTime, e.maxWanderTime);
        timeSpentThisWander = 0;
        wanderDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    public override void Update()
    {
        e.Move(wanderDirection);
        timeSpentThisWander += Time.deltaTime;
        if(timeSpentThisWander > currentWanderTimer)
        {
            NewWander();
        }
    }


}

public class AggressiveState : EnemyAIState
{
    public AggressiveState(Enemy enemy)
    {
        e = enemy;
        stateType = EnemyAIStateType.Aggressive;
        Enter();
    }
    public override void Enter()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
    }


}