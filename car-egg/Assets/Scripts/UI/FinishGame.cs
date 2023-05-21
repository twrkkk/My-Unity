using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGame : MonoBehaviour
{
    [SerializeField] private GameObject _finishGamePanel;
    [SerializeField] private GameObject _nextLevelButton;
    [SerializeField] private Star[] _stars;

    public IEnumerator ShowFinishPanel(float delay, bool isWin = true)
    {
        yield return new WaitForSeconds(delay);
        _finishGamePanel.SetActive(true);

        int starsCount = PlayerScores.instance.GetLevelStarsCount(isWin);

        if (starsCount > 0)
            _nextLevelButton.SetActive(true);
        else
            _nextLevelButton.SetActive(false);

        for (int i = 0; i < starsCount; i++)
        {
            _stars[i].ActivateStar();
            yield return new WaitForSeconds(0.2f);
        }
    }
}
