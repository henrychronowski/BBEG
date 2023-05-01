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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
