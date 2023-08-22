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

    // Expand regions to view events

    #region Attacks

    public event Action<HitData> onAttackConnected;
    public void AttackConnected(HitData data)
    {
        if(onAttackConnected != null)
        {
            onAttackConnected(data);
        }

        if(data.mRecipient.transform.tag != "Minion")
        {
            // Process the damage from here so 
            data.ProcessDamage();
            HitProcessed(data);
        }
    }

    // Meant to run immediately after AttackConnected is completed
    public event Action<HitData> onHitProcessed;

    public void HitProcessed(HitData data)
    {
        if (onHitProcessed != null)
        {
            onHitProcessed(data);
        }
    }

    // Meant to run immediately after AttackConnected is completed
    public event Action<HitData> onAttackFinalized;

    public void HitFinalized(HitData data)
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
    #endregion

    #region Enemy

    public event Action<Enemy, EnemyAIState> onAIStateChanged;


    public void AIStateChanged(Enemy e, EnemyAIState t)
    {
        if(onAIStateChanged != null)
        {
            onAIStateChanged(e, t);
        }
    }

    #endregion

    #region Dungeon


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

    public event Action<RoomInfo> onRoomEntered;

    public void RoomEntered(RoomInfo r)
    {
        if (onRoomEntered != null)
        {
            onRoomEntered(r);
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
    #endregion

    #region Player/Party

    public event Action<ArtifactBase> onArtifactAdded;

    public void AddArtifact(ArtifactBase a)
    {
        if(onArtifactAdded != null)
        {
            onArtifactAdded(a);
        }
    }
    
    public event Action onLeaderDeath;

    public void LeaderDeath()
    {
        if (onLeaderDeath != null)
        {
            onLeaderDeath();
        }
    }

    public event Action<Character> onCharacterDeath;

    public void CharacterDeath(Character c)
    {
        if (onCharacterDeath != null)
        {
            onCharacterDeath(c);
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

    public event Action<Minion> onSacrificeMinion;

    public void SacrificeMinion(Minion m)
    {
        if (onSacrificeMinion != null)
        {
            onSacrificeMinion(m);
        }
    }
    #endregion

    #region Rewards

    public event Action<Rarity> onKeyReward;

    public void KeyReward(Rarity r)
    {
        if (onKeyReward != null)
        {
            onKeyReward(r);
        }
    }

    public event Action<Rarity> onGoldReward;

    public void GoldReward(Rarity r)
    {
        if (onGoldReward != null)
        {
            onGoldReward(r);
        }
    }

    public event Action<int> onHealReward;

    public void HealReward(int r)
    {
        if (onHealReward != null)
        {
            onHealReward(r);
        }
    }
    public event Action<Rarity> onArtifactReward;

    public void ArtifactReward(Rarity r)
    {
        if (onArtifactReward != null)
        {
            onArtifactReward(r);
        }
    }

    public event Action<Rarity> onMinionReward;

    public void MinionReward(Rarity r)
    {
        if (onMinionReward != null)
        {
            onMinionReward(r);
        }
    }

    #endregion

    #region Misc


    public event Action onDemoEndReached;

    public void DemoEndReached()
    {
        if (onDemoEndReached != null)
        {
            onDemoEndReached();
        }
    }

    public event Action<string> onCageBreakFail;

    public void CageBreakFail()
    {
        if(onCageBreakFail != null)
        {
            onCageBreakFail("CageBreakFail");
        }
    }

    public event Action<Minion> onCageBreakSuccess;

    public void CageBreakSuccess(Minion m)
    {
        if (onCageBreakSuccess != null)
        {
            onCageBreakSuccess(m);
        }
    }

    #endregion

    #region Sound

    public event Action<string> onPlaySound;
    public void PlaySound(string _name)
    {
        if (onPlaySound != null)
        {
            onPlaySound(_name);
        }
    }

    #endregion

    #region Dialogue

    public event Action onNodeStart;
    public void NodeStart()
    {
        if (onNodeStart != null)
        {
            onNodeStart();
        }
    }

    public event Action onNodeComplete;
    public void NodeComplete()
    {
        if (onNodeComplete != null)
        {
            onNodeComplete();
        }
    }

    public event Action onDialogueComplete;
    public void DialogueComplete()
    {
        if (onDialogueComplete != null)
        {
            onDialogueComplete();
        }
    }

    public event Action onCommand;
    public void Command()
    {
        if (onCommand != null)
        {
            onCommand();
        }
    }

    #endregion

    #region Template
    public event Action onTestEvent;
    public void TestEvent()
    {
        if (onTestEvent != null)
        {
            onTestEvent();
        }
    }
    #endregion
}
