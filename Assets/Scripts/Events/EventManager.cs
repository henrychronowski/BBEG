using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // Whenever you add an event listener to a script in Start(), you MUST remove it in OnDestroy()
    // Example:
    /*    
    void Start()
    {
        EventManager.instance.onAttackConnected += PlayAttackSound;
    }

    private void OnDestroy()
    {
        EventManager.instance.onAttackConnected -= PlayAttackSound;
    }
    */
    public static EventManager instance;

    private void Awake()
    {
        instance = this;
    }

    public event Action<HitData> onAttackConnected;

    public void AttackConnected(HitData data)
    {
        if(onAttackConnected != null)
        {
            onAttackConnected(data);
            HitProcessed(data);
        }
    }

    // Meant to run immediately after AttackConnected is completed
    // Should only ever contain HitCharacter so that onAttackConnected can modify HitData
    // before the damage gets applied
    public event Action<HitData> onHitProcessed;

    public void HitProcessed(HitData data)
    {
        if (onHitProcessed != null)
        {
            onHitProcessed(data);
        }
    }

    public event Action<Attack> onAttackStarted;

    public void AttackStarted(Attack atk)
    {
        if (onAttackStarted != null)
        {
            onAttackStarted(atk);
        }
    }

    public event Action<Enemy, EnemyAIState> onAIStateChanged;

    public void AIStateChanged(Enemy e, EnemyAIState t)
    {
        if(onAIStateChanged != null)
        {
            onAIStateChanged(e, t);
        }
    }

    // Fires when new minion joins the party
    public event Action<Minion> onNewMinionAdded;
    public void NewMinionAdded(Minion m)
    {
        if(onNewMinionAdded != null)
        {
            onNewMinionAdded(m);
        }
    }

    public event Action<Transform> onGenerationComplete;
    public void GenerationComplete(Transform spawn)
    {
        if (onGenerationComplete != null)
        {
            onGenerationComplete(spawn);
        }
    }

    public event Action onStairsReached;
    public void StairsReached()
    {
        if (onStairsReached != null)
        {
            onStairsReached();
        }
    }

    public event Action onLoadNewFloor;
    public void LoadNewFloor()
    {
        if (onLoadNewFloor != null)
        {
            onLoadNewFloor();
        }
    }

    // Fires when the screen goes black during the floor transition
    public event Action onTransitionInProgress;
    public void TransitionInProgress()
    {
        if (onTransitionInProgress != null)
        {
            onTransitionInProgress();
        }
    }

    // Fires when the transition finishes
    public event Action onTransitionComplete;
    public void TransitionComplete()
    {
        if (onTransitionComplete != null)
        {
            onTransitionComplete();
        }
    }

    public event Action<RoomInfo> roomEntered;

    public void RoomEntered(RoomInfo r)
    {
        if (roomEntered != null)
        {
            roomEntered(r);
        }
    }

    public event Action<RoomInfo> roomCleared;

    public void RoomCleared(RoomInfo r)
    {
        if (roomCleared != null)
        {
            roomCleared(r);
        }
    }

    // Useful for events that do not pass variables, prevents needing to make unique functions for those
    public void FireTypelessEvent(Action typelessAction)
    {
        if(typelessAction != null)
        {
            typelessAction();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
