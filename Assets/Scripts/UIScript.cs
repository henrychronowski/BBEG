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
