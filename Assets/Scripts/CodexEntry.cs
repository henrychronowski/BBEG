using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CodexEntry : MonoBehaviour
{
    //Note: This is for the selecting the codex entries in the codex itself.
    //The script called "Codex" is used for storing codex data.

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI loreText;
    public Image sprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Entry(Codex entry)
    {
        if(entry.isUnlocked)
        {
            titleText.text = entry.entryTitle;
            loreText.text = entry.entryDesc;
            sprite.sprite = entry.entrySprite;
        }
    }
}
