using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private Sprite _damagedBlock;
    [SerializeField] private int _destroyedScore;
    [SerializeField] private ParticleSystem _destroyEffect;
    [SerializeField] BlockType _type;
    private SpriteRenderer _image;
    private float _startHealth;
    private bool _isDestroyed;
    private AudioSource _destroySound;
    public PolygonCollider2D _collider;

    enum BlockType
    {
        Wood,
        Ice, 
        Rock
    }

    private void SetupSound()
    {
        switch (_type)
        {
            case BlockType.Wood:
                _destroySound = GameSounds.instance.WoodDestroy;
                break;
                case BlockType.Ice:
                _destroySound = GameSounds.instance.IceDestroy;
                break;
            case BlockType.Rock:
                _destroySound = GameSounds.instance.RockDestroy;
                break;
        }
    }

    private void Start()
    {
        _isDestroyed = false;
        _startHealth = _health;
        PlayerScores.instance.CalculateLevelScores(_destroyedScore);
        _image = GetComponent<SpriteRenderer>();
        _collider = GetComponent<PolygonCollider2D>();
        SetupSound();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _health -= collision.relativeVelocity.magnitude;
        if(_health <= 0 && !_isDestroyed)
        {
            _isDestroyed = true;
            PlayerScores.instance.Scores += _destroyedScore;
            if (GameSounds.instance.PlaySounds)
                _destroySound.Play();
            StartCoroutine(Destroy());
            //Destroy(gameObject);
        }
        if (_health < _startHealth*(3f/5))
            _image.sprite = _damagedBlock;
        //if (collision.relativeVelocity.magnitude > health)
    }

    private IEnumerator Destroy()
    {
        _destroyEffect.Play();
        _image.enabled = false;
        if (_collider) _collider.enabled = false;
        //yield return new WaitUntil(() => !_destroyEffect.isPlaying);
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
