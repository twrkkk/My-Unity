using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenuPanel;
    [SerializeField] private GameObject _pauseMenuButton;
    [SerializeField] private GameObject _finishGamePanel;
    [SerializeField] private GameObject _starImages;
    [SerializeField] private Image _finishStarsImage;
    [SerializeField] private GameObject _nextLevelButton;
    [SerializeField] private BirdSpawn _birdSpawn;
    [SerializeField] private Star[] _stars;

    [SerializeField] private Animator _pauseAnim;

    [SerializeField] GameObject _MusicOffCross;
    [SerializeField] GameObject _SoundOffCross;

    private void SetUpSoundsButtons()
    {
        _MusicOffCross.SetActive(!GameSounds.instance.PlayMusic);
        _SoundOffCross.SetActive(!GameSounds.instance.PlaySounds);
    }

    public void MusicButton()
    {
        GameSounds.instance.PlayMusic = !GameSounds.instance.PlayMusic;
        SetUpSoundsButtons();
    }

    public void SoundsButton()
    {
        GameSounds.instance.PlaySounds = !GameSounds.instance.PlaySounds;
        SetUpSoundsButtons();
    }

    public void PauseGame()
    {
        _pauseAnim.SetTrigger("Pause");
        Time.timeScale = 0f;
        _pauseMenuPanel.SetActive(true);
        _pauseMenuButton.SetActive(false);
        SetUpSoundsButtons();
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
        _pauseMenuPanel.SetActive(false);
        _pauseMenuButton.SetActive(true);
    }

    public IEnumerator ShowFinishPanel()
    {
        _birdSpawn.DeactivateBirds();

        yield return new WaitForSeconds(3f);

        _finishGamePanel.SetActive(true);
        _starImages.SetActive(false);
        //PlayerScores.instance.StarImage.enabled = false;

        //_finishStarsImage.sprite = PlayerScores.instance.StarSprites[PlayerScores.instance.Stars];
        if (PlayerScores.instance.Stars > 0)
            _nextLevelButton.SetActive(true);
        else
            _nextLevelButton.SetActive(false);

        int StarsCount = PlayerScores.instance.Stars;
        for (int i = 0; i < StarsCount; i++)
        {
            _stars[i].ActivateStar();
            yield return new WaitForSeconds(0.2f);
        }
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
