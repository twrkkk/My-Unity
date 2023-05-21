using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenuPanel;
    [SerializeField] private GameObject _pauseMenuButton;
    [SerializeField] private Animator _pauseAnim;
    [SerializeField] private PrometeoCarController _prometeoCarController;

    [SerializeField] GameObject _MusicOffCross;
    [SerializeField] GameObject _SoundOffCross;

    private void Start()
    {
        SetUpSoundsButtons();
    }

    private void SetUpSoundsButtons()
    {
        _MusicOffCross.SetActive(!AudioManager.Instance.PlayMusic);
        _SoundOffCross.SetActive(!AudioManager.Instance.PlaySounds);
    }

    public void MusicButton()
    {
        AudioManager.Instance.PlayMusic = !AudioManager.Instance.PlayMusic;
        if (AudioManager.Instance.PlayMusic)
            AudioManager.Instance.Play("Music");
        else
            AudioManager.Instance.Stop("Music");

        SetUpSoundsButtons();
    }

    public void SoundsButton()
    {
        AudioManager.Instance.PlaySounds = !AudioManager.Instance.PlaySounds;
        _prometeoCarController.useSounds = AudioManager.Instance.PlaySounds;
        _prometeoCarController.carEngineSound.Play();
        SetUpSoundsButtons();
    }

    public void PauseGame()
    {
        _pauseAnim.SetTrigger("Pause");
        Time.timeScale = 0f;
        _pauseMenuPanel.SetActive(true);
        _pauseMenuButton.SetActive(false);
        SetUpSoundsButtons();

        //Debug.Log(AudioManager.Instance.PlaySounds + "sounds");
        //Debug.Log(AudioManager.Instance.PlayMusic + "music");
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
        _pauseMenuPanel.SetActive(false);
        _pauseMenuButton.SetActive(true);
    }

    public void OpenMenu()
    {
        ContinueGame();
        SceneTransition.SceneToSwitch("Menu");
    }

    public void Again()
    {
        ContinueGame();
        SceneTransition.SceneToSwitch(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        ContinueGame();
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneTransition.SceneToSwitch(++index);
    }
}
