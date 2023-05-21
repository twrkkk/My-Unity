using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUICtrl : MonoBehaviour
{
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundsSlider;
    [SerializeField] private Text _coinText;
    [SerializeField] private Text _scoreText;

    private void Start()
    {
        InitSliders();
        _coinText.text = "" + (PlayerPrefs.HasKey(PlayerScores.instance.CoinsKey) ? PlayerPrefs.GetInt(PlayerScores.instance.CoinsKey) : 0);
        _scoreText.text = "" + (PlayerPrefs.HasKey(PlayerScores.instance.ScoresKey) ? PlayerPrefs.GetInt(PlayerScores.instance.ScoresKey) : 0);
    }

    public void StartGame()
    {
        SceneTransition.SceneToSwitch(1);
    }

    public void OpenSetting()
    {
        _settingsPanel.SetActive(true);
        InitSliders();
    }

    public void CloseSettings()
    {
        _settingsPanel.SetActive(false);
    }

    private void InitSlider(Slider slider, string key)
    {
        //if (!slider)
        //    slider = GameObject.FindGameObjectWithTag(tag).GetComponent<Slider>();
        //switch (tag)
        //{
        //    case "Master":
        //    slider.onValueChanged.AddListener(_ => AudioManager.instance.ChangeMasterVolume(slider.value));
        //        break;
        //    case "Music":
        //        slider.onValueChanged.AddListener(_ => AudioManager.instance.ChangeMusicVolume(slider.value));
        //        break;
        //    case "Sounds":
        //        slider.onValueChanged.AddListener(_ => AudioManager.instance.ChangeSoudsVolume(slider.value));
        //        break;
        //}
        //slider.value = PlayerPrefs.GetFloat(tag);

    }

    public void InitSliders()
    {
        //if(!_masterSlider)
        //    InitSlider(_masterSlider, "Master");
        //if(!_musicSlider)
        //    InitSlider(_musicSlider, "Music");
        //if(!_soundsSlider)
        //    InitSlider(_soundsSlider, "Sounds");
        _masterSlider.onValueChanged.AddListener(_ => AudioManager.instance.ChangeMasterVolume(_masterSlider.value));
        _musicSlider.onValueChanged.AddListener(_ => AudioManager.instance.ChangeMusicVolume(_musicSlider.value));
        _soundsSlider.onValueChanged.AddListener(_ => AudioManager.instance.ChangeSoudsVolume(_soundsSlider.value));

        if (PlayerPrefs.HasKey(AudioManager.instance.MasterKey))
            _masterSlider.value = PlayerPrefs.GetFloat(AudioManager.instance.MasterKey);
        if (PlayerPrefs.HasKey(AudioManager.instance.MusicKey))
            _musicSlider.value = PlayerPrefs.GetFloat(AudioManager.instance.MusicKey); 
        if (PlayerPrefs.HasKey(AudioManager.instance.SoundsKey))
            _soundsSlider.value = PlayerPrefs.GetFloat(AudioManager.instance.SoundsKey);

    }
}
