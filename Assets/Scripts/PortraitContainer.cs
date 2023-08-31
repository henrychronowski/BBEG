using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPortraitContainer", menuName = "ScriptableObjects/Portrait Container", order = 1)]
[SerializeField]
public class PortraitContainer : ScriptableObject
{
    public List<string> keys;
    public List<Sprite> sprites;
    
    Dictionary<string, Sprite> portraits;

    public Sprite GetSprite(string key)
    {
        int index = keys.IndexOf(key);
        if(sprites.Count <= index)
        {
            Debug.LogError(name + " has a mismatched Portrait Container, key = " + key);
            return null;
        }
        return sprites[index];
    }

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
