using UnityEngine;
using System.Collections;

public class DragBird : MonoBehaviour {
	
	public float stretchLimit = 1.0f;
	public LineRenderer stringBack;
	public LineRenderer stringFront;
	public Camera cam;
	
	private SpringJoint2D spring;
	private bool clicked;
	private Transform slingshot;
	private Ray mouseRay;
	private Ray leftRay;
	private float stretchSquare;
	private float radius;
	private Vector2 velocityX;
	
	void Awake () {
		spring = GetComponent<SpringJoint2D> ();
		slingshot = spring.connectedBody.transform;
		
	}
	
	void Start () {
		//StringSetup ();
		mouseRay = new Ray(slingshot.position, Vector3.zero);
		leftRay = new Ray(stringFront.transform.position, Vector3.zero);
		stretchSquare = stretchLimit * stretchLimit;
		CircleCollider2D circleColl = GetComponent<Collider2D>() as CircleCollider2D;
		radius = circleColl.radius;
		
	}
	
	void Update () {
		if (clicked)
			Dragging ();
	}

	
	void OnMouseDown () {
		spring.enabled = false;
		clicked = true;
		GetComponent<Rigidbody2D>().isKinematic = true;
	}
	
	void OnMouseUp () {
		spring.enabled = true;
		GetComponent<Rigidbody2D>().isKinematic = false;
		clicked = false;
	}
	
	void Dragging () {

		var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
		Debug.Log(mousePos);
		Vector2 fromSlingshot = mousePos - slingshot.position;
		
		if (fromSlingshot.sqrMagnitude > stretchSquare) {
			mouseRay.direction = fromSlingshot;
			mousePos = mouseRay.GetPoint(stretchLimit);
		}
		
		mousePos.z = 0f;
		transform.position = mousePos;
	}

}
