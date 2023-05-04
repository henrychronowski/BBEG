using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Replace this with something more robust later
    // 
    public AudioClip[] clips;
    public AudioSource source;
    void PlayAttackSound(HitData data)
    {
        source.PlayOneShot(clips[data.mAttack.soundId]);

    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.onAttackConnected += PlayAttackSound;
    }

    private void OnDestroy()
    {
        EventManager.instance.onAttackConnected -= PlayAttackSound;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
