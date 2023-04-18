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

    // Update is called once per frame
    void Update()
    {
        
    }
}
