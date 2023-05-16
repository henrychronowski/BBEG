using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum ArtifactBehaviorType
{
    None,
    LowHPAttackBoost,
    HealUponNewMinion
}

public class ArtifactManager : MonoBehaviour
{
    public List<ArtifactBase> activeArtifacts = new List<ArtifactBase>();
    [SerializeField] bool addArtifactOnStart;
    [SerializeField] ArtifactBase healArtifact;
    [SerializeField] ArtifactBase damageArtifact;

    bool AddArtifact(ArtifactBase artifact)
    {
        if(activeArtifacts.Contains(artifact) && !artifact.canStack)
        {
            return false;
        }
        activeArtifacts.Add(artifact);
        artifact.ApplyBuff();

        ArtifactBehaviorBase behaviorComponent = null;
        switch(artifact.behavior)
        {
            case ArtifactBehaviorType.LowHPAttackBoost:
                {
                    behaviorComponent = gameObject.AddComponent<LowHPAttackBoost>();
                    break;
                }
            case ArtifactBehaviorType.HealUponNewMinion:
                {
                    behaviorComponent = gameObject.AddComponent<HealUponNewMinion>();
                    break;
                }
        }
        artifact.behaviorComponent = behaviorComponent;

        return true;
    }

    void RemoveArtifact(ArtifactBase artifact)
    {
        artifact.RemoveBuff();

        activeArtifacts.Remove(artifact);

        if(artifact.behavior != ArtifactBehaviorType.None)
            Destroy(artifact.behaviorComponent);
    }

    void ApplyBuffsToNewMinion(Minion m)
    {
        foreach(ArtifactBase artifactBase in activeArtifacts)
        {
            if(artifactBase.buff != null)
            {
                if (m.tribe == artifactBase.targetTribe || artifactBase.targetTribe == Tribe.Neutral)
                {
                    m.activeBuffs.Add(artifactBase.buff);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.onNewMinionAdded += ApplyBuffsToNewMinion;
        if(addArtifactOnStart)
            AddArtifact(healArtifact);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            RemoveArtifact(activeArtifacts[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AddArtifact(healArtifact);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AddArtifact(damageArtifact);
        }
    }
}
