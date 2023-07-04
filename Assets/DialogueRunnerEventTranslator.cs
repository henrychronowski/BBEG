using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueRunnerEventTranslator : MonoBehaviour
{
    public void OnNodeStart()
    {
        EventManager.instance.NodeStart();
    }

    public void OnNodeComplete()
    {
        EventManager.instance.NodeComplete();
    }

    public void OnDialogueComplete()
    {
        EventManager.instance.DialogueComplete();
    }

    public void OnCommand()
    {
        EventManager.instance.Command();
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
