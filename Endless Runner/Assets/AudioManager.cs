using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioMixerGroup _masterMixer;
    [SerializeField] private AudioSource _collectCoin;
    [SerializeField] private AudioSource _jump;
    [SerializeField] private AudioSource _swipe;
    [SerializeField] private AudioSource _stumble;
    [SerializeField] private AudioSource _pressUI;

    private string MASTER_KEY = "Master";
    private string MUSIC_KEY = "Music";
    private string SOUNDS_KEY = "Sounds";

    public string MasterKey { get { return MASTER_KEY; } }
    public string MusicKey { get { return MUSIC_KEY; } }
    public string SoundsKey { get { return SOUNDS_KEY; } }

    public AudioMixerGroup MasterAudio { get { return _masterMixer; } }
    public AudioSource CollectCoin { get { return _collectCoin; } }
    public AudioSource Jump { get { return _jump; } }
    public AudioSource Swipe { get { return _swipe; } }
    public AudioSource Stumble { get { return _stumble; } }
    public AudioSource PressUI { get { return _pressUI; } }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(this);

        
    }

    public void ChangeMasterVolume(float volume)
    {
        _masterMixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-40, 0, volume));
        PlayerPrefs.SetFloat(MASTER_KEY, volume);
    }

    public void ChangeMusicVolume(float volume)
    {
        _masterMixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-40, 0, volume));
        PlayerPrefs.SetFloat(MUSIC_KEY, volume);
    }

    public void ChangeSoudsVolume(float volume)
    {
        _masterMixer.audioMixer.SetFloat("SFXVolume", Mathf.Lerp(-40, 0, volume));
        _masterMixer.audioMixer.SetFloat("UIVolume", Mathf.Lerp(-40, 0, volume));
        PlayerPrefs.SetFloat(SOUNDS_KEY, volume);   
    }



}
