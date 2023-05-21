using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerScores : MonoBehaviour
{
    static public PlayerScores instance;

    [SerializeField] private Text _scoresText;
    [SerializeField] private Star[] _stars;
    [SerializeField] private TimeManager _timeManager;
    private int levelIndex;
    private int currentStarsNum;

    private int _scores;

    private Vector3 _startTextScale;
    public int Scores { get { return _scores; } set 
        { 
            _scores = value;
            StartCoroutine(UpdateScoreText());
        } 
    }

    public int Stars => currentStarsNum;
    public Star[] StarSprites => _stars;

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

    private void Start()
    {
        levelIndex = int.Parse(SceneManager.GetActiveScene().name);
    }

    public void ResetScore()
    {
        _scores = 0;
        currentStarsNum = 0;
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

        SetLevelStars(3);

        yield return new WaitForSeconds(0.1f);

        _scoresText.transform.localScale = _startTextScale;
    }

    public int GetLevelStarsCount(bool isWin)
    {
        if (!isWin) return 0;

        float percentage = (_timeManager.StartDuration - _timeManager.TimeRemaining) / _timeManager.StartDuration;
        int result;
        if (percentage < 0.8f)
        {
            SetLevelStars(3);
            result = 3;
        }
        else if (percentage < 0.9f)
        {
            SetLevelStars(2);
            result = 2;
        }
        else
        {
            SetLevelStars(1);
            result = 1;
        }

        Debug.Log(percentage + " " + result);

        return result;
    }
}
