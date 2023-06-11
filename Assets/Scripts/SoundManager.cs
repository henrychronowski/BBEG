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
        source.PlayOneShot(clips[2]);

    }

    void PlayAttackStartSound(Attack atk)
    {
        source.PlayOneShot(clips[atk.soundId]);
    }


    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.onAttackStarted += PlayAttackStartSound;
        EventManager.instance.onHitProcessed += PlayAttackSound;
    }

    private void OnDestroy()
    {
        EventManager.instance.onAttackStarted -= PlayAttackStartSound;
        EventManager.instance.onHitProcessed -= PlayAttackSound;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
