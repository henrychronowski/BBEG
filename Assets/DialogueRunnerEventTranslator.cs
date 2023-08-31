using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class DialogueRunnerEventTranslator : MonoBehaviour
{
    public static DialogueRunner instance { get; private set; }

    public Image portrait;
    [SerializeField] List<PortraitContainer> portraitContainers;
    public Sprite GetPortrait(string character, string expression)
    {
        return portraitContainers.Find(p => p.name == character).GetSprite(expression);
    }

    [YarnCommand("portrait")]
    public void SetPortrait(string character, string expression)
    {
        portrait.enabled = true;
        portrait.sprite = GetPortrait(character, expression);
    }

    [YarnCommand("clear")]
    public void ClearPortrait()
    {
        portrait.enabled = false;
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        else
            instance = GetComponent<DialogueRunner>();
    }
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
        EventManager.instance.onDialogueComplete += ClearPortrait;
    }

    private void OnDestroy()
    {
        EventManager.instance.onDialogueComplete -= ClearPortrait;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
