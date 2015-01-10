using UnityEngine;
using System.Collections;

public class RobotController : MonoBehaviour {

	public float maxSpeed = 10.0f;
	bool facingRight = true;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		float move = Input.GetAxis("Horizontal");
		rigidbody2D.velocity = new Vector2 (maxSpeed * move, rigidbody2D.velocity.y);

	}

	void flip()
	{
		facingRight = !facingRight;
		Vector3 curScale = transform.localScale;
		curScale.x *= -1;

		transform.localScale = curScale;
	}
}
