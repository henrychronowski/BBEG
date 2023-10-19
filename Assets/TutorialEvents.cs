using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class TutorialEvents : MonoBehaviour
{
    [SerializeField] GameObject set1;
    [SerializeField] GameObject set2;
    [SerializeField] GameObject set3;
    [SerializeField] Character tutorialGuy;
    [SerializeField] List<string> keys;
    [SerializeField] GameObject stairs;
    [SerializeField] int activeKeyIndex = 0;
    [SerializeField] int minionRequirement = 2;
    [YarnCommand("Set1")]
    public void EnableSet1()
    {
        set1.SetActive(true);
        
    }

    [YarnCommand("Set2")]
    public void EnableSet2()
    {
        set2.SetActive(true);

    }

    [YarnCommand("Set3")]
    public void EnableSet3()
    {
        set3.SetActive(true);
    }

    [YarnCommand("Kill")]
    public void KillPlayer()
    {
        PlayerCharacterManager.instance.leader.Hit(99);

    }

    [YarnCommand("Heal")]
    public void HealPlayer()
    {
        PlayerCharacterManager.instance.leader.Heal(99);

    }

    void NextPhase(Character c = null)
    {
        activeKeyIndex++;
        tutorialGuy.yarnKey = keys[activeKeyIndex];
        tutorialGuy.DialogueStart(PlayerCharacterManager.instance.leader);
    }

    void MinionPhaseClearCheck(Minion m)
    {
        if(PlayerCharacterManager.instance.minions.Count == minionRequirement)
        {
            NextPhase();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.onSacrificeMinion += NextPhase;
        EventManager.instance.onCharacterDeath += NextPhase;
        EventManager.instance.onNewMinionAdded += MinionPhaseClearCheck;
    }
    private void OnDestroy()
    {
        EventManager.instance.onSacrificeMinion -= NextPhase;
        EventManager.instance.onCharacterDeath -= NextPhase;
        EventManager.instance.onNewMinionAdded -= MinionPhaseClearCheck;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
