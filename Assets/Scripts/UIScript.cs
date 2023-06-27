using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Text tempCurr;
    public Text permCurr;

    public TextMeshProUGUI hpText;

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
        manager = PlayerCharacterManager.instance;


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
        UpdatePortraits();
    }

    void UpdateText()
    {
        tempCurr.text = "Keys: " + manager.keys.ToString();
        permCurr.text = "Gold: " + manager.gold.ToString();
        hpText.text = manager.leader.currHealth + "/" + manager.leader.GetMaxHealth();
    }

    void UpdateHealth()
    {
        leaderHP.value = manager.leader.currHealth;
        leaderHP.maxValue = manager.leader.GetMaxHealth();
        leaderHP.fillRect.gameObject.SetActive(leaderHP.value > 0);
    }

    void UpdatePortraits()
    {
        //for (int i = 0; i < manager.minions.Count; i++)
        //{
        //    if (manager.minions[i] != null)
        //    {
        //        minionPortraits[i].gameObject.SetActive(true);
        //        minionPortraits[i].sprite = manager.minions[i].portrait;
        //    }
        //    else
        //    {
        //        minionPortraits[i].gameObject.SetActive(false);
        //    }
        //}
        for (int i = 0; i < minionPortraits.Count; i++)
        {
            if(i < manager.minions.Count)
            {
                if (manager.minions[i] != null)
                {
                    minionPortraits[i].gameObject.SetActive(true);
                    minionPortraits[i].sprite = manager.minions[i].portrait;
                }
            }
            else
            {
                minionPortraits[i].gameObject.SetActive(false);
            }
        }
    }
}
