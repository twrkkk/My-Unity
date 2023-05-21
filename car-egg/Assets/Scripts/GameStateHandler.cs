using MilkShake;
using MilkShake.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Start,
    Win,
    Lose
}

public class GameStateHandler : MonoBehaviour
{
    public static GameStateHandler Instance;
    public Transform CheckPointsParent;
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private PrometeoCarController _carController;
    [SerializeField] private FinishGame _finishGame;
    private GameState _gameState;
    private int _allCheckPointsCount;
    private int _colletcedCheckpointsCount;

    public GameState GameState => _gameState;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }


    void Start()
    {
        _carController.CanMove = false;
        if (CheckPointsParent)
            _allCheckPointsCount = CheckPointsParent.childCount;
    }

    public void StartGame()
    {
        _carController.CanMove = true;
        Debug.Log("The game is start!");
        _gameState = GameState.Start;
        _timeManager.StartTimer();
    }

    public void WinGame()
    {
        if (_gameState == GameState.Win) return;
        if (_colletcedCheckpointsCount < _allCheckPointsCount) return;

        Debug.Log("You're win!");
        _carController.CanMove = false;
        StartCoroutine(_finishGame.ShowFinishPanel(1f));
        _gameState = GameState.Win;
        _timeManager.TimerIsRunning = false;

    }

    public void LoseGame()
    {
        if (_gameState == GameState.Lose) return;

        Debug.Log("You're lose!");
        _carController.CanMove = false;
        StartCoroutine(_finishGame.ShowFinishPanel(2f, false));
        _gameState = GameState.Lose;
        _timeManager.TimerIsRunning = false;
    }

    public void CollectCheckPoint()
    {
        Debug.Log("Collect checkpoint!");
        _colletcedCheckpointsCount++;
    }
}
