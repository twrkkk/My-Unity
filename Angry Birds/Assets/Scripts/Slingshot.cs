using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
	static public Slingshot instance;
	[SerializeField] private Transform _startBirdPos;	
	[SerializeField] private float releaseTime = .15f;
	[SerializeField] private LineRenderer stringBack;
	[SerializeField] private LineRenderer stringFront;
	[SerializeField] private float _lineWidth;
	[SerializeField] private Trajectory _trajectory;
	//[SerializeField] private Transform _lineEndPos;
	[SerializeField] private GameObject _birdPrefab;
    [SerializeField] private BirdSpawn _birdSpawn;
	[SerializeField] private GameObject _trailDot;
	[SerializeField] private int _dotsCount;
	[SerializeField] private Transform _dotsParent;
	private GameObject[] _trailDots;
	private int _currPointIndex;
	private GameObject _currentBird;
	private Bird _bird;
	private Rigidbody2D _currentBirdRB;
	private SpringJoint2D _springJoint;
	private Ray leftRay;
	private float radius;
	private CircleCollider2D _circleCollider;

	private bool _showStrings;
	private bool _isPlay;

	private GameObject _prevBird;

	public Trajectory Trajectory { get { return _trajectory; } }
	public SpringJoint2D SprindJoint { get { return _springJoint; } }
	public GameObject[] TrailDots { get { return _trailDots; } } 

	private void Awake()
	{
		if (instance == null)
			instance = this;
		else
		{
			Destroy(gameObject);
		}
	}
	void Start()
	{
		_isPlay = true;
		CreateBird(_startBirdPos.position);
		StringSetup();
		_currPointIndex = 0;
		_trailDots = new GameObject[_dotsCount];
        for (int i = 0; i < _dotsCount; i++)
        {
			_trailDots[i] = Instantiate(_trailDot, Vector3.zero, Quaternion.identity);
			_trailDots[i].SetActive(false);
			_trailDots[i].transform.parent = _dotsParent;

		}
		//StringUpdate();
		leftRay = new Ray(stringFront.transform.position, Vector3.zero);
		_prevBird = null;
	}

    private void Update()
	{// 
		if (_bird.isPressed && _showStrings && _isPlay)
			StringUpdate();
	}

    void StringSetup()
	{
		ActivateLine();
		stringBack.SetPosition(0, stringBack.transform.position);
		stringFront.SetPosition(0, stringFront.transform.position);
		stringBack.SetPosition(1, stringBack.transform.position);
		stringFront.SetPosition(1, stringFront.transform.position);
		stringBack.SetWidth(_lineWidth, _lineWidth);
		stringFront.SetWidth(_lineWidth, _lineWidth);
		//stringBack.sortingOrder = 1;
		//stringFront.sortingOrder = 3;
	}

	public void StringUpdate()
	{
		Vector2 projectile = _currentBird.transform.position - stringFront.transform.position;
		leftRay.direction = projectile;
		Vector3 hold = leftRay.GetPoint(projectile.magnitude + radius);
		stringBack.SetPosition(1, hold);
		stringFront.SetPosition(1, hold);
	}

	void CreateBird(Vector3 pos)
    {
		_currentBird = _birdSpawn.NextBird();//Instantiate(prefab, pos, Quaternion.identity);
		if (_currentBird)
		{
			_currentBird.transform.position = pos;
			ActivateLine();
			StringSetup();
			_springJoint = _currentBird.GetComponent<SpringJoint2D>();
			_springJoint.enabled = true;
			_currentBird.GetComponent<Bird>()._isInteractible = true;
			radius = _currentBird.GetComponent<CircleCollider2D>().radius * 2f;
			_circleCollider = _currentBird.GetComponent<CircleCollider2D>();
			_currentBirdRB = _currentBird.GetComponent<Rigidbody2D>();
			_bird = _currentBird.GetComponent<Bird>();
			//Debug.Log(_currentBird.GetComponent<Bird>().gameObject);
			//_springJoint.distance = 1f;
		}
		else 
		{
			//birds queue is empty 
			//finish game
			_isPlay = false;
			Debug.Log("FINISH GAME");
		}
	}

	public IEnumerator ShowTrail(Bird bird)
	{
		if (_currPointIndex > _trailDots.Length - 1) yield return null;
		if(_currPointIndex == 0)
        {
			_trailDots[_currPointIndex].transform.position = bird.transform.position;
			_trailDots[_currPointIndex++].SetActive(true);
        }

		yield return new WaitForSeconds(9/Mathf.Max(_currentBirdRB.velocity.x, _currentBirdRB.velocity.y));
		if (!bird.IsHit)
		{
			_trailDots[_currPointIndex].transform.position = bird.transform.position;
			_trailDots[_currPointIndex++].SetActive(true);
			for (int i = _currPointIndex; i < _trailDots.Length; i++)
			{
				_trailDots[i].transform.position = bird.transform.position;
			}
	
			StartCoroutine(ShowTrail(bird));
		}
	}

	public void ResetTrail()
    {
        foreach (GameObject point in _trailDots)
        {
			point.SetActive(false);
        }
		_currPointIndex = 0;
	}

	public IEnumerator Release(GameObject bird)
	{
		yield return new WaitForSeconds(releaseTime);

		bird.GetComponent<SpringJoint2D>().enabled = false;
		DeactivateLine();
		_trajectory.Hide();
		//this.enabled = false;

		Bird newBird = bird.GetComponent<Bird>();
		newBird._isInteractible = false;
		//if (_prevBird != newBird)
		{
			ResetTrail();
			newBird.StartCoroutine(ShowTrail(newBird));
		}

		if(GameSounds.instance.PlaySounds)
        {
			GameSounds.instance.ReleaseBird.Play();
		}

		//_prevBird = bird;

		yield return new WaitForSeconds(4f);

		DeactivateLine();
		GetOutBird();

		yield return new WaitForSeconds(1f);

		PlayerScores.instance.BirdCount--;
		Destroy(_currentBird);
		CreateBird(_startBirdPos.position);
	}

	//public IEnumerator DestroyBird()
	//   {
	//	yield return new WaitForSeconds(2f);

	//	DeactivateLine();
	//	Destroy(_currentBird);
	//	CreateBird(_startBirdPos.position);
	//}

	private void GetOutBird()
	{
		_circleCollider.enabled = false;
		//_currentBird.GetComponent<Rigidbody2D>().AddForce(_currentBird.transform.up);
	}


	public void ActivateLine()
	{
		_showStrings = true;
		stringBack.enabled = true;
		stringFront.enabled = true;
	}

	void DeactivateLine()
	{
		_showStrings = false;
		stringBack.enabled = false;
		stringFront.enabled = false;
	}
}
