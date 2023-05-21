using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerScores : MonoBehaviour
{
    static public PlayerScores instance;

    [SerializeField] private Text _scoresText;
    [SerializeField] private Text _enemyText;
    [SerializeField] private int levelIndex;
    [SerializeField] private Star[] _stars;
    [SerializeField] private PauseMenu _pauseMenu;
    private int currentStarsNum;
    private int currentImageIndex;
    private int _levelMaxScores;

    private int _scores;
    private int _enemiesCount;
    private int _enemiesDestroyed;
    private int _birdCount;

    private Vector3 _startTextScale;
    public int Scores { get { return _scores; } set 
        { 
            _scores = value;
            StartCoroutine(UpdateScoreText());
        } 
    }
    public int EnemiesCount { get { return _enemiesCount; } set { _enemiesCount = value; _enemyText.text = "" + _enemiesDestroyed + '/' + _enemiesCount; } }
    public int EnemiesDestroyed { get { return _enemiesDestroyed; } set { 
            _enemiesDestroyed = value; 
            _enemyText.text = "" + _enemiesDestroyed + '/' + _enemiesCount;
            if (_enemiesDestroyed == _enemiesCount)
            {
               StartCoroutine(_pauseMenu.ShowFinishPanel());
               
            }
        } 
    }

    public int Stars => currentStarsNum;
    public Star[] StarSprites => _stars;

    public int BirdCount { get { return _birdCount; }

        set 
        {
            _birdCount = value;
            if(_birdCount < 1)
            {
                StartCoroutine(_pauseMenu.ShowFinishPanel());
                //_starImage.enabled = false;
            }
        }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        ResetScore();
        //if (currentStarsNum != 3) SetLevelStars(0);
        _startTextScale = _scoresText.transform.localScale;

    }

    public void ResetScore()
    {
        _scores = 0;
        _enemiesCount = 0;
        _enemiesDestroyed = 0;
        currentStarsNum = 0;
        currentImageIndex = 0;
        _levelMaxScores = 0;
    }

    public void CalculateLevelScores(int score)
    {
        _levelMaxScores += score;
    }

    public void BackButton()
    {
        SceneManager.LoadScene("00_Level Selection");
    }

    public void SetLevelStars(int _starsNum)
    {
        currentStarsNum = _starsNum;
        if (currentStarsNum > PlayerPrefs.GetInt("Lv" + levelIndex))
        {
            PlayerPrefs.SetInt("Lv" + levelIndex, _starsNum);
        }

        for (int i = 0; i < currentStarsNum; i++)
        {
            if (!_stars[i].IsActivate)
                _stars[i].ActivateStar();
        }

        //_stars[currentStarsNum - 1].ActivateStar();
        //BackButton();
    }


    private IEnumerator UpdateScoreText()
    {
        _scoresText.text = "" + _scores;
        _scoresText.transform.localScale = _startTextScale * 1.15f;

        if (_scores > 0.65 * _levelMaxScores)
        {
            if(currentStarsNum != 3) SetLevelStars(3);
        }
        else if (_scores > 0.5 * _levelMaxScores)
        {
            if (currentStarsNum != 2) SetLevelStars(2);
        }
        else if (_scores > 0.3 * _levelMaxScores)
        {
            if (currentStarsNum != 1) SetLevelStars(1);
        }

        yield return new WaitForSeconds(0.1f);

        _scoresText.transform.localScale = _startTextScale;
    }
}
