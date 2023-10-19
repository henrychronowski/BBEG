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

    public CodexManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager.gameObject.GetComponent<CodexManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Entry(int entryNum)
    {
        if(manager.codices[entryNum].isUnlocked)
        {
            titleText.text = manager.codices[entryNum].entryTitle;
            loreText.text = manager.codices[entryNum].entryDesc;
            sprite.sprite = manager.codices[entryNum].entrySprite;
        }
    }
}
