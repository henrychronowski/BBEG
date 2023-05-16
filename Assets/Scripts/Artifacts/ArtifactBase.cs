using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tribe
{ 
    Neutral,
    Demon,
    Undead,
    Squimbus
}

public enum ArtifactTarget
{ 
    All,
    Leader,
    Minion
}


[CreateAssetMenu(fileName = "NewArtifact", menuName = "ScriptableObjects/New Artifact", order = 1)]
[SerializeField]
public class ArtifactBase : ScriptableObject
{
    // ArtifactBase contains funcionality for simple stat boosts
    public bool canStack = true;
    public Buff buff;

    // What characters does it apply to? Leader only? Leader + all minions? Leader + demon minions?
    public ArtifactTarget affectedCharacterTypes = ArtifactTarget.All;

    // Set to neutral if tribe is irrelevant
    public Tribe targetTribe = Tribe.Neutral;
    public ArtifactBehaviorType behavior;

    // Reference to the component that runs the artifact behavior.
    // If we want to remove artifacts without removing everything this will be useful
    public Component behaviorComponent;
    public bool ApplyBuff()
    {
        if(buff == null)
            return false;

        PlayerCharacterManager pcm = PlayerCharacterManager.instance;
        if(affectedCharacterTypes == ArtifactTarget.Leader || affectedCharacterTypes == ArtifactTarget.All)
        {
            PlayerCharacterManager.instance.leader.activeBuffs.Add(buff);
        }

        if (affectedCharacterTypes == ArtifactTarget.Minion || affectedCharacterTypes == ArtifactTarget.All)
        {
            foreach (Minion m in pcm.minions)
            {
                if(m.tribe == targetTribe || targetTribe == Tribe.Neutral)
                {
                    m.activeBuffs.Add(buff);
                }
            }
        }

        return true;
    }

    public bool RemoveBuff()
    {
        if (buff == null)
            return false;

        PlayerCharacterManager pcm = PlayerCharacterManager.instance;

        pcm.leader.activeBuffs.Remove(buff);

        foreach (Minion m in pcm.minions)
        {
            m.activeBuffs.Remove(buff);
        }

        return true;
    }

    private void OnEnable()
    {
        //affectedCharacterTypes = new CharacterType[5];
    }

    private void OnValidate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
