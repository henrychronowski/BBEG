using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Leader : Character
{
    // PlayerCharacterManager sends input to this character
    // Minions check the Leader's state to determine their actions

    // Start is called before the first frame update
    void Start()
    {
        state = new IdleState(this);

    }

    // Update is called once per frame
    void Update()
    {
        state.Update();
    }
}
