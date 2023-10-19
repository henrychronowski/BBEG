using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealUponNewMinion : ArtifactBehaviorBase
{
    public float healingPercentage = .2f;
    
    void ApplyHeal(Minion m)
    {
        PlayerCharacterManager pcm = PlayerCharacterManager.instance;
        pcm.leader.Heal(Mathf.RoundToInt(pcm.leader.GetMaxHealth() * healingPercentage));
    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.onNewMinionAdded += ApplyHeal;
    }

    private void OnDestroy()
    {
        EventManager.instance.onNewMinionAdded -= ApplyHeal;
    }

    // Update is called once per frame
    void Update()
    {

    }
}