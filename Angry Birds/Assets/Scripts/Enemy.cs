using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public GameObject deathEffect;

	public float health;
	private bool _isDied;

	[SerializeField] private Sprite _leftBruise;
	[SerializeField] private SpriteRenderer _leftEye;
	[SerializeField] private Sprite _rightBruise;
	[SerializeField] private SpriteRenderer _rightEye;
	[SerializeField] private int _destroyedScore;

	private float _startHealth;

	void Start()
	{
		_isDied = false;
		_startHealth = health;
		PlayerScores.instance.EnemiesCount++;
		PlayerScores.instance.CalculateLevelScores(_destroyedScore);
	}

	void OnCollisionEnter2D(Collision2D colInfo)
	{
		health -= colInfo.relativeVelocity.magnitude;
		if (health <= 0 && !_isDied)
		{
			_isDied = true;
			if (GameSounds.instance.PlaySounds)
				GameSounds.instance.AddStar.Play();
			Die();
			PlayerScores.instance.Scores += _destroyedScore;
		}
		else if(health < _startHealth/2)
        {
			_leftEye.sprite = _leftBruise;
			_rightEye.sprite = _rightBruise;

		}
		else if (health < _startHealth/3)
        {
			_leftEye.sprite = _leftBruise;
        }
	}

	void Die()
	{
		if(deathEffect)
			Instantiate(deathEffect, transform.position, Quaternion.identity);

		PlayerScores.instance.EnemiesDestroyed++;
		//EnemiesAlive--;
		//if (EnemiesAlive <= 0)
		//	Debug.Log("LEVEL WON!");

		Destroy(gameObject);
	}
}
