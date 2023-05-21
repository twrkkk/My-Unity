using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InfimaGames.LowPolyShooterPack;
using UnityEngine.InputSystem;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private Movement _playerMovement;
    [SerializeField] private Character _character;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Animator _animator;
    [SerializeField] private CameraLook _cameraLook;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private GameObject _firstWeapon, _secondWeapon;
    private GameObject _currentWeapon;

    private bool _gameIsFinished;

    private int _totalEnemyCount;
    private int _currentEnemyCount;
    public int TotalEnemyCount { get { return _totalEnemyCount; } set { _totalEnemyCount = value; Debug.LogFormat("enemy count {0}", _totalEnemyCount); } }
    public int CurrentEnemyCount { get { return _currentEnemyCount; } set { _currentEnemyCount = value; EnemyCountHandler(); } }

    static public LevelHandler instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void DiePlayer()
    {
        //_playerMovement.canMove = false;
        _playerMovement.enabled = false;
        _character.enabled = false;
        _playerInput.enabled = false;
        _animator.enabled = false;
        _cameraLook.enabled = false;
        _rb.constraints = RigidbodyConstraints.None;
        _currentWeapon = _firstWeapon.activeInHierarchy ? _firstWeapon : _secondWeapon;
        Rigidbody weaponRB = _currentWeapon.AddComponent<Rigidbody>();
        weaponRB.interpolation = RigidbodyInterpolation.Interpolate;
        weaponRB.velocity = (_currentWeapon.transform.forward + _currentWeapon.transform.up) * 5f;
        weaponRB.AddTorque(new Vector3(Random.Range(-90, 90), Random.Range(-90, 90), Random.Range(-90, 90)));
    }

    private void FinishGame()
    {
        if (_gameIsFinished) return;
        Debug.Log("finish game");
        _gameIsFinished = true;
    }

    private void EnemyCountHandler()
    {
        if (_currentEnemyCount <= 0)
            FinishGame();
    }

}
