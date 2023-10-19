using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Codex : MonoBehaviour
{
    //Note: This is for storing codex data
    //"Codex Entry" is for showing codex entries in the codex
    public bool isUnlocked;
    public string entryTitle;
    public string entryDesc;
    public Sprite entrySprite;

    public Button button;
    public Text buttonName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckTrue();
    }

    void CheckTrue()
    {
        if(!isUnlocked)
        {
            buttonName.text = "???";
            button.interactable = false;
        }
        else
        {
            buttonName.text = entryTitle;
            button.interactable = true;
        }
    }
}
