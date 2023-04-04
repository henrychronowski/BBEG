using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Text tempCurr;
    public Text permCurr;

    public Slider leaderHP;
    public Slider minion1HP;
    public Slider minion2HP;
    public Slider minion3HP;

    public Image leaderImage;
    public Image minion1Image;
    public Image minion2Image;
    public Image minion3Image;


    List<Slider> minionHPContainer;
    List<Image> minionPortraits;

    public PlayerCharacterManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<PlayerCharacterManager>();

        tempCurr.text = "Gold: " + manager.tempCurr.ToString();
        permCurr.text = "Gems: " + manager.tempCurr.ToString();

        leaderHP.maxValue = manager.leader.health;
        leaderImage.sprite = manager.leader.portrait;

        minionHPContainer = new List<Slider>();
        minionHPContainer.Add(minion1HP);
        minionHPContainer.Add(minion2HP);
        minionHPContainer.Add(minion3HP);

        minion1HP.gameObject.SetActive(false);
        minion2HP.gameObject.SetActive(false);
        minion3HP.gameObject.SetActive(false);

        minionPortraits = new List<Image>();
        minionPortraits.Add(minion1Image);
        minionPortraits.Add(minion2Image);
        minionPortraits.Add(minion3Image);

        minion1Image.gameObject.SetActive(false);
        minion2Image.gameObject.SetActive(false);
        minion3Image.gameObject.SetActive(false);

        for (int i = 0; i < manager.minions.Count; i++)
        {
            minionHPContainer[i].gameObject.SetActive(true);
            minionHPContainer[i].maxValue = manager.minions[i].health;

            minionPortraits[i].gameObject.SetActive(true);
            minionPortraits[i].sprite = manager.minions[i].portrait;
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
        //Temp values until we get current and max hp values
        leaderHP.value = manager.leader.health;
        for(int i = 0; i < minionHPContainer.Count; i++)
        {
            minionHPContainer[i].value = manager.minions[i].health;
        }
    }
}
