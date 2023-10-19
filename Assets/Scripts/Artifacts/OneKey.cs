using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneKey : ArtifactBehaviorBase
{

    public override void OnCollect()
    {
        EventManager.instance.KeyReward(Rarity.Common);
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
