using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

// Copied directly from Planetary Planter's sound manager made by Mac
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.7f;

    [Range(0.5f, 1.5f)]
    public float pitch = 1f;

    [Range(0f, 0.5f)]
    public float pitchRand = 0.1f;

    private AudioSource source;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void Play()
    {
        source.volume = volume;
        source.pitch = pitch * (1 + Random.Range(-pitchRand / 2, pitchRand / 2));
        source.PlayOneShot(clip);
    }

    public void Pause()
    {
        source.Pause();
    }

    public void Play(float newPitch)
    {
        source.volume = volume;
        source.pitch = newPitch;
        source.PlayOneShot(clip);
    }

    public void Play(Vector3 location)
    {
        source.volume = volume;
        source.pitch = pitch * (1 + Random.Range(-pitchRand / 2, pitchRand / 2)); ;
        //source.PlayOneShot(clip);

        AudioSource.PlayClipAtPoint(clip, location, source.volume);
    }
}

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;

    [SerializeField]
    Sound[] sounds;

    // Replace this with something more robust later
    // 
    public AudioClip[] clips;
    public AudioSource source;
    void PlayAttackHitSound(HitData data)
    {
        if(data.mRecipient.tag == "Breakable")
        {

        }
        else
        {
            PlaySound("HitSound");
        }
    }

    void PlayAttackStartSound(Attack atk)
    {
        PlaySound(atk.soundName);
    }

    private void Awake()
    {
        EventManager.instance.onAttackConnected += PlayAttackHitSound;
        if (instance != null)
        {
            Debug.LogError("Extra Audio Manager in scene");
        }

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.onPlaySound += PlaySound;
        EventManager.instance.onCageBreakFail += PlaySound;

        EventManager.instance.onAttackStarted += PlayAttackStartSound;
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }
    }

    private void OnDestroy()
    {
        EventManager.instance.onAttackStarted -= PlayAttackStartSound;
        EventManager.instance.onAttackConnected -= PlayAttackHitSound;
        EventManager.instance.onPlaySound -= PlaySound;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {

                sounds[i].Play();
                return;
            }
        }
        Debug.LogWarning("SoundManager: Sound not found in sounds list:" + _name);
    }


    public void PlaySoundWithPitch(string _name, float newPitch)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play(newPitch);
                return;
            }
        }
        Debug.LogWarning("SoundManager: Sound not found in sounds list:" + _name);
    }

    public void PlaySoundAtLocation(string _name, Vector3 location)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play(location);
                return;
            }
        }
        Debug.LogWarning("SoundManager: Sound not found in sounds list:" + _name);
    }

    public void PauseSound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Pause();
                return;
            }
        }
        Debug.LogWarning("SoundManager: Sound not found in sounds list:" + _name);
    }
}
