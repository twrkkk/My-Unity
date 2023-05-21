using UnityEngine;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;
    public static AudioManager Instance;
    public bool PlayMusic
    {
        get
        {
            return _playMusic;
        }
        set
        {
            _playMusic = value;
        }
    }
    public bool PlaySounds
    {
        get
        {
            return _playSounds;
        }
        set
        {
            _playSounds = value;
        }
    }
    public bool _playMusic;
    public bool _playSounds;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        foreach (var sound in Sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }


        PlayMusic = true;
        PlaySounds = true;  
    }

    public void Play(string name)
    {
        if (!(name == "Music" && PlayMusic || name != "Music" && PlaySounds)) return;

        Sound sound = Sounds.FirstOrDefault(x => x.name == name);
        if (sound == null)
        {
            Debug.LogError($"Sound {name} not found!");
            return;
        }
        sound.source.Play();
    }

    public void Stop(string name)
    {
        Sound sound = Sounds.FirstOrDefault(x => x.name == name);
        if (sound == null)
        {
            Debug.LogError($"Sound {name} not found!");
            return;
        }
        sound.source.Stop();
    }
}
