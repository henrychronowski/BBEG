using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RewardType
{
    Gold,
    Healing,
    Key,
    Artifact,
    Minion
}

public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Cursed,
    Legendary
}

[SerializeField]
public class RewardSpawner : MonoBehaviour
{
    int goldWeight;
    int healWeight;
    int keyWeight;
    int artifactWeight;
    int minionWeight;
    public int[] weights;

    [SerializeField]
    public GameObject emptyArtifact;


    public List<ArtifactBase> artifactPool;
    public List<GameObject> minionPool;
    [SerializeField] int baseHealing = 2;


    public int[] rarityWeights;

    public int roomClearDropChance;
    public static List<RewardType> RewardTypes = new List<RewardType>(5);
    public Dictionary<RewardType, int> rewards = new Dictionary<RewardType, int>();
    void RollForReward(RoomInfo r)
    {
        int rng = Random.Range(0, 100);
        if (roomClearDropChance > rng)
        {
            int totalWeight = 0;
            foreach (int w in weights)
                totalWeight += w;

            int random = Random.Range(1, totalWeight + 1);

            int currentWeight = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                currentWeight += weights[i];
                if (random <= currentWeight)
                {
                    PickReward((RewardType)i, r);
                    return;
                }
            }
        }

    }

    void PickReward(RewardType type, RoomInfo r)
    {
        Debug.Log("Spawning " + type.ToString());
        Rarity rarity = PickRarity();
        switch (type)
        {
            case RewardType.Artifact:
                {
                    int random = Random.Range(0, artifactPool.Count);
                    GameObject newArtifact = Instantiate(emptyArtifact, r.rewardSpawn);
                    newArtifact.GetComponent<CollectableArtifact>().AssignArtifact(artifactPool[random], rarity);
                    break;
                }
            case RewardType.Healing:
                {
                    EventManager.instance.HealReward(baseHealing);

                    break;
                }
            case RewardType.Key:
                {
                    EventManager.instance.KeyReward(rarity);
                    break;
                }
            case RewardType.Minion:
                {
                    EventManager.instance.MinionReward(rarity);
                    Instantiate(minionPool[0], r.rewardSpawn);
                    break;
                }
            case RewardType.Gold:
                {
                    EventManager.instance.GoldReward(rarity);
                    break;
                }
        }
    }

    Rarity PickRarity()
    {
        int totalWeight = 0;
        foreach (int w in rarityWeights)
            totalWeight += w;

        int random = Random.Range(1, totalWeight + 1);

        int currentWeight = 0;
        for (int i = 0; i < rarityWeights.Length; i++)
        {
            currentWeight += rarityWeights[i];
            if (random <= currentWeight)
            {
                return (Rarity)i;

            }
        }
        // If this is returning cursed theres a problem
        return Rarity.Cursed;
    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.roomCleared += RollForReward;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        
    }
}
