using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScores : MonoBehaviour
{
    private int _coins;
    private int _steps;

    private string COINS_KEY = "Coins";
    private string SCORES_KEY = "Scores";

    public string CoinsKey { get { return COINS_KEY; } }
    public string ScoresKey { get { return SCORES_KEY; } }

    public static PlayerScores instance;    

    public int Coins { get { return _coins; } set { _coins = value; } }
    public int Steps { get { return _steps; } set { _steps = value; } }

    private void Awake()
    {
        instance = this;    
    }

    private void Start()
    {
        if(!PlayerPrefs.HasKey(COINS_KEY))
            PlayerPrefs.SetInt(COINS_KEY, 0);
        if(!PlayerPrefs.HasKey(SCORES_KEY))
            PlayerPrefs.SetInt(SCORES_KEY, 0);

        PlayerPrefs.Save();
    }

    public void UpdateValues()
    {
        PlayerPrefs.SetInt(SCORES_KEY, Mathf.Max(_steps, PlayerPrefs.GetInt(SCORES_KEY)));
        PlayerPrefs.SetInt(COINS_KEY, PlayerPrefs.GetInt(COINS_KEY) + _coins);
    }
}
