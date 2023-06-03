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

    public int[] rarityWeights;

    int roomClearDropChance;
    public static List<RewardType> RewardTypes = new List<RewardType>(5);
    public Dictionary<RewardType, int> rewards = new Dictionary<RewardType, int>();
    void RollForReward(RoomInfo r)
    {
        int rng = Random.Range(0, 100);
        if (roomClearDropChance < rng)
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
                    //Debug.Log("Generating Room " + roomArray[i].name + " of weight " + roomWeight[i].ToString() + ". Random = " + random.ToString());
                    PickReward((RewardType)i, r);
                }
            }
        }

    }

    void PickReward(RewardType type, RoomInfo r)
    {
        Debug.Log("Spawning " + type.ToString());
        Rarity rarity = PickRarity();
        switch(type)
        {
            case RewardType.Artifact:
                {
                    int random = Random.Range(0, artifactPool.Count);
                    GameObject newArtifact = Instantiate(emptyArtifact, r.rewardSpawn);
                    newArtifact.GetComponent<CollectableArtifact>().AssignArtifact(artifactPool[random], rarity);
                    break;
                }
            case RewardType.Minion:
                {

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
        if(Input.GetKeyDown(KeyCode.R))
        {
            RollForReward(PlayerCharacterManager.instance.activeRoom);
        }
    }

    private void Awake()
    {
        
    }
}
