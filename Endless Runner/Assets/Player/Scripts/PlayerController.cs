using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _sideSpeed;
    [SerializeField] private byte _currentLine = 1;
    [SerializeField] private float distBtwnLine;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform _groundCheckPos;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private Animator _animator;

    private bool isJump = false;
    private SwipeController swipeController;
    private Rigidbody _rb;
    public Rigidbody RB { get { return _rb; } }
    private bool _startRunning = false;
    [HideInInspector]
    public bool isPlay;

    public static PlayerController Instance;
    public Animator PlayerAnimator { get { return _animator; } }

    [Header("Finish Screen")]
    [SerializeField] private Text _coinsText;
    [SerializeField] private Text _stepText;
    public GameObject FinishPanel;
    private bool addDist = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        isPlay = true;
        swipeController = SwipeController.instance;
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

    }

    private void Update()
    {
        if (!isPlay) return;
     
        if(!addDist)
        {
            addDist = true;
            StartCoroutine(CalculateSteps());
        }
        CheckGround();
        //Debug.Log(swipeController.swipeLeft.ToString() + ' ' + swipeController.swipeRight.ToString() + ' ' + swipeController.swipeUp.ToString() + ' ' + swipeController.swipeDown.ToString());
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        ControlJump();
        ControlLine();

        if(_speed < 16f && isPlay)
        {
            SetSpeed(_speed * 1.0001f);
            _jumpForce /= 1.00001f;
        }
    }

    IEnumerator CalculateSteps()
    {
        PlayerScores.instance.Steps++;
        GameUICtrl.instance.UpdateTextScores(PlayerScores.instance.Steps);
        yield return new WaitForSeconds(1 / GetSpeed());
        addDist = false;
    }

    private void FixedUpdate()
    {
        if (!isPlay) return;

        if (isJump)
        {
            Jump();
        }
            
    }

    private void AddForce(Vector3 force)
    {
        _rb.AddForce(force);
    }

    private void Jump()
    {
        _animator.SetTrigger("isJumping");
        AddForce(Vector3.up * _jumpForce);
        AudioManager.instance.Jump.Play();
        isJump = false;
    }

    private void CheckGround()
    {
        _isGrounded = Physics.OverlapSphere(_groundCheckPos.position, 0.3f).Length > 1;
    }

    public void SetSpeed(float newSpeed)
    {
        _speed = newSpeed;

    }

    public float GetSpeed()
    {
        return _speed;
    }

    public void SetAnim(string name, bool value)
    {
        _animator.SetBool(name, value);
    }

    private void ControlLine()
    {
        if (swipeController.swipeLeft && _currentLine > 0)
        {
            --_currentLine;
            AudioManager.instance.Swipe.Play();
        }
        else if (swipeController.swipeRight && _currentLine < 2)
        {
            ++_currentLine;
            AudioManager.instance.Swipe.Play();
        }

        if (Mathf.Abs(transform.position.x - (_currentLine - 1) * distBtwnLine) > 0.05f)
        {
            Vector3 newPos = new Vector3((_currentLine - 1) * distBtwnLine, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPos, _sideSpeed * Time.deltaTime);
        }
    }

    private void ControlJump()
    {
        if (swipeController.swipeUp && _isGrounded)
            isJump = true;//Jump();
        if (swipeController.swipeDown && !_isGrounded)
        {
            AddForce(Vector3.down * _jumpForce);
            _animator.speed = 2f;
        }
        else if (_isGrounded)
            _animator.speed = 1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6) // coin
        {
            AudioManager.instance.CollectCoin.Play();
            ObjectPool.Instance.Coins.AddToPool(other.GetComponent<Coin>());
            PlayerScores.instance.Coins++;
            GameUICtrl.instance.UpdateTextCoins(PlayerScores.instance.Coins);
        }
        else if(other.gameObject.layer == 7) // obstacle
        {
            //Debug.Log("Hit");
            StartCoroutine(FinishGame());
            PlayerScores.instance.UpdateValues();
        }
    }

    IEnumerator FinishGame()
    {
        AudioManager.instance.Stumble.Play();
        SetSpeed(0);
        GameUICtrl.instance.DeativatePauseButton();
        _animator.SetBool("isStumble", true);
        isPlay = false;
        yield return new WaitForSeconds(1);
        ShowFinishScreen();
    }

    private void ShowFinishScreen()
    {
        _coinsText.text = PlayerScores.instance.Coins.ToString();
        _stepText.text = PlayerScores.instance.Steps.ToString();
        FinishPanel.SetActive(true);
    }

}
