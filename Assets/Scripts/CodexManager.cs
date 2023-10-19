using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodexManager : MonoBehaviour
{
    public List<Codex> codices;
    public static CodexManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        codices.AddRange(GetComponents<Codex>());
    }

    public void Unlock(string title)
    {
        Codex entry = FindEntryByTitle(title);

        if(entry == null)
        {
            return;
        }

        if(!entry.isUnlocked)
        {
            entry.isUnlocked = true;
        }
        else
        {
            Debug.Log(title + " unlocked already");
        }
    }

    public Codex FindEntryByTitle(string title)
    {
        for(int i = 0; i < codices.Count; i++)
        {
            if(codices[i].entryTitle == title)
            {
                return codices[i];
            }
        }
        return null;
    }

    public bool IsUnlocked(string title)
    {
        return FindEntryByTitle(title).isUnlocked;
    }
}
