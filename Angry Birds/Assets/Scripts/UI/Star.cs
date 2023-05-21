using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Star : MonoBehaviour
{
    [SerializeField] private Sprite _blankStar;
    [SerializeField] private Sprite _fullStar;
    [SerializeField] private ParticleSystem _showEffect;
    private Image _starImage;
    private bool _isActivate;

    public bool IsActivate => _isActivate;

    private void Awake()
    {
        _starImage = GetComponent<Image>();
        _starImage.sprite = _blankStar;
    }

    public void ActivateStar()
    {
        if (_isActivate) return;
        if (GameSounds.instance.PlaySounds)
            GameSounds.instance.AddStar.Play();
        _isActivate = true;
        _showEffect.Play();
        _starImage.sprite = _fullStar;
    }
}
