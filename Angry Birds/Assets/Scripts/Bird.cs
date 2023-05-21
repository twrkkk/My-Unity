using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
	[SerializeField] private LineRenderer _trail;
	public Rigidbody2D rb;
	public float maxDragDistance = 2f;
	private Rigidbody2D hook;
	private float radius;
	public bool isPressed = false;
	public bool _isInteractible;

	public bool IsHit;

    private void Awake()
    {
		_isInteractible = false; //to call delete method one time
        
    }
    private void Start()
    {
		IsHit = false;
		hook = Slingshot.instance.GetComponent<Rigidbody2D>();	
		radius = GetComponent<CircleCollider2D>().radius;
	}

    void Update()
	{
		if (isPressed && _isInteractible)
		{
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			if (Vector3.Distance(mousePos, hook.position) > maxDragDistance)
				rb.position = hook.position + (mousePos - hook.position).normalized * maxDragDistance;
			else
				rb.position = mousePos;

			Slingshot.instance.Trajectory.Show();
			Slingshot.instance.Trajectory.ShowTrajectory(Slingshot.instance.transform.position, (Slingshot.instance.transform.position - transform.position)*4.6f);
			//Slingshot.instance.StringUpdate();
			//Slingshot.instance.Trajectory.UpdateDots(rb.position, -Slingshot.instance.SprindJoint.reactionForce);
		}

		//StringUpdate();

	}

	void OnMouseDown()
	{
		if (!_isInteractible) return;
		isPressed = true;
		rb.isKinematic = true;
		//Slingshot.instance.Trajectory.Show();
		//Slingshot.instance.ActivateLine();
	}

	void OnMouseUp()
	{
		if (!_isInteractible) return;
		isPressed = false;
		rb.isKinematic = false;
		//Slingshot.instance.Trajectory.Hide();

		StartCoroutine(Slingshot.instance.Release(gameObject));
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
		if(collision.gameObject.layer != 7)
        {
			IsHit = true;
        }
	}
}

