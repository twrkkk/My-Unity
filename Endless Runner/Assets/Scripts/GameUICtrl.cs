using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUICtrl : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _pauseButton;

    [SerializeField] private Text _coinsText, _stepsText;
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundsSlider;

    public static GameUICtrl instance;

    private void Start()
    {
        instance = this;
        InitSliders();
    }
    public void PauseGame()
    {
        PlayerController.Instance.PlayerAnimator.speed = 0f;
        PlayerController.Instance.RB.isKinematic = true;
        PlayerController.Instance.isPlay = false;
        _pauseMenu.SetActive(true);
        _pauseButton.SetActive(false);
        AudioManager.instance.PressUI.Play();
        InitSliders();
    }

    public void ContinueGame()
    {
        PlayerController.Instance.PlayerAnimator.speed = 1f;
        PlayerController.Instance.RB.isKinematic = false;
        PlayerController.Instance.isPlay = true;
        _pauseMenu.SetActive(false);
        _pauseButton.SetActive(true);
        AudioManager.instance.PressUI.Play();
    }

    public void ExitGameMenu()
    {
        //load menu scene
        AudioManager.instance.PressUI.Play();
        PlayerScores.instance.UpdateValues();
        SceneTransition.SceneToSwitch(0);
    }

    public void Restart()
    {
        AudioManager.instance.PressUI.Play();
        SceneTransition.SceneToSwitch(1);
    }

    public void UpdateTextCoins(int coins)
    {
        _coinsText.text = "" + coins;
    }

    public void UpdateTextScores(int steps)
    {
        _stepsText.text = "" + steps;
    }

    public void DeativatePauseButton()
    {
        _pauseButton.SetActive(false);
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
