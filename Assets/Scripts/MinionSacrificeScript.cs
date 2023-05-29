using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinionSacrificeScript : MonoBehaviour
{
    public GameObject sacrificeMenu;
    public List<Image> minionImages;
    public Image minion1Image;
    public Image minion2Image;
    public Image minion3Image;

    public PlayerCharacterManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<PlayerCharacterManager>();
        minionImages = new List<Image>();
        minionImages.Add(minion1Image);
        minionImages.Add(minion2Image);
        minionImages.Add(minion3Image);

        minion1Image.gameObject.SetActive(false);
        minion2Image.gameObject.SetActive(false);
        minion3Image.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenScreen()
    {
        sacrificeMenu.gameObject.SetActive(true);
        for(int i = 0; i < minionImages.Count; i++)
        {
            if(manager.minions[i] != null)
            {
                minionImages[i].gameObject.SetActive(true);
                minionImages[i].sprite = manager.minions[i].portrait;
            }
            else
            {
                minionImages[i].gameObject.SetActive(false);
            }
        }
        Time.timeScale = 0f;
    }

    public void CloseScreen()
    {
        sacrificeMenu.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Sacrifice(Image minionImage)
    {
        for(int i = 0; i < manager.minions.Count; i++)
        {
            if(manager.minions[i] != null && manager.minions[i].portrait == minionImage.sprite)
            {
                Destroy(manager.minions[i].gameObject);
                manager.minions[i] = null;
                break;
            }
            else
            {
                continue;
            }
        }
    }
}
