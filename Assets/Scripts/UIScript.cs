using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Text tempCurr;
    public Text permCurr;

    public Slider leaderHP;
    public Image leaderImage;

    public Image minion1Image;
    public Image minion2Image;
    public Image minion3Image;

    List<Image> minionPortraits;

    public PlayerCharacterManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<PlayerCharacterManager>();

        tempCurr.text = "Gold: " + manager.tempCurr.ToString();
        permCurr.text = "Gems: " + manager.tempCurr.ToString();

        leaderHP.maxValue = manager.leader.GetMaxHealth();
        leaderHP.value = manager.leader.currHealth;
        leaderImage.sprite = manager.leader.portrait;

        minionPortraits = new List<Image>();
        minionPortraits.Add(minion1Image);
        minionPortraits.Add(minion2Image);
        minionPortraits.Add(minion3Image);

        minion1Image.gameObject.SetActive(false);
        minion2Image.gameObject.SetActive(false);
        minion3Image.gameObject.SetActive(false);

        for (int i = 0; i < manager.minions.Count; i++)
        {
            if(manager.minions[i] != null)
            {
                minionPortraits[i].gameObject.SetActive(true);
                minionPortraits[i].sprite = manager.minions[i].portrait;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
        UpdateHealth();
    }

    void UpdateText()
    {
        tempCurr.text = "Gold: " + manager.tempCurr.ToString();
        permCurr.text = "Gems: " + manager.permCurr.ToString();
    }

    void UpdateHealth()
    {
        leaderHP.value = manager.leader.currHealth;
        leaderHP.maxValue = manager.leader.GetMaxHealth();
    }
}
