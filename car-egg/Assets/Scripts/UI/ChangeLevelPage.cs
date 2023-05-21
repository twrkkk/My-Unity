using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLevelPage : MonoBehaviour
{
    [SerializeField] private List<GameObject> _list = new List<GameObject>();
    [SerializeField] private GameObject _leftButton;
    [SerializeField] private GameObject _rightButton;
    private int _currentPageInd;

    private void Start()
    {
        DesableAll();
        ChangePage(0);
    }

    private void DesableAll()
    {
        foreach(var g in _list)
            g.SetActive(false);
    }
    public void ChangePage(int step)
    {
        _currentPageInd += step;
        _currentPageInd = Mathf.Clamp(_currentPageInd, 0, _list.Count - 1);

        if(_currentPageInd == 0)
        {
            _leftButton.SetActive(false);
            _rightButton.SetActive(true);
        }
        else if(_currentPageInd == _list.Count - 1)
        {
            _rightButton.SetActive(false);
            _leftButton.SetActive(true);
        }
        else
        {
            _leftButton.SetActive(true);
            _rightButton.SetActive(true);
        }

        DesableAll();
        _list[_currentPageInd].SetActive(true);
    }
}
