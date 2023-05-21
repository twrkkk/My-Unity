using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameSounds : MonoBehaviour
{
    static public GameSounds instance;

    [SerializeField] private AudioMixerGroup _masterMixer;
    [SerializeField] private AudioSource _music;
    [SerializeField] private AudioSource _releaseBird;
    [SerializeField] private AudioSource _woodDestroy;
    [SerializeField] private AudioSource _iceDestroy;
    [SerializeField] private AudioSource _rockDestroy;
    [SerializeField] private AudioSource _enemyDestroy;
    [SerializeField] private AudioSource _addStar;
    [SerializeField] private AudioSource _buttonPressed;

    private bool _playMusic = true;
    private bool _playSounds = true;

    public AudioMixerGroup Mixer => _masterMixer;
    public AudioSource Music => _music;
    public AudioSource ReleaseBird => _releaseBird;
    public AudioSource WoodDestroy => _woodDestroy;
    public AudioSource IceDestroy => _iceDestroy;
    public AudioSource RockDestroy => _rockDestroy;
    public AudioSource EnemyDestroy => _enemyDestroy;
    public AudioSource AddStar => _addStar;
    public AudioSource ButtonPressed => _buttonPressed;

    public bool PlayMusic { 
        get 
        { 
            return _playMusic; 
        } 
        set 
        {
            _playMusic = value;
            if (_playMusic)
                _music.Play();
            else _music.Stop();
        }
    }
    public bool PlaySounds { 
        get 
        { 
            return _playSounds; 
        } 
        set 
        {
            _playSounds = value;
        } 
    }

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //_music.Play();
        PlayMusic = true;
        PlaySounds = true;
    }
}
