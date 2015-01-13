using UnityEngine;
using System.Collections;

public class RobotController : MonoBehaviour {

	public float maxSpeed = 10.0f;
	bool facingRight = true;

	Animator anim;
	bool grounded = false;
	float groundRadius = 0.2f;

	public Transform groundCheck;
	public LayerMask whatIsGround;
	public float jumpForce = 700f;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);
		if (!grounded) {
			Debug.Log("grounded: " + grounded);		
		}
		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);
		if (rigidbody2D.velocity.y != 0) {
			Debug.Log ("vSpeed: " + rigidbody2D.velocity.y);
				
		}


		float move = Input.GetAxis("Horizontal");
		rigidbody2D.velocity = new Vector2 (maxSpeed * move, rigidbody2D.velocity.y);

		anim.SetFloat ("speed", Mathf.Abs (move));

		// If the input is moving the player right and the player is facing left...
		if (move > 0 && !facingRight)
			// ... flip the player.
			Flip();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (move < 0 && facingRight)
			// ... flip the player.
			Flip();

	}

	void Update()
	{
		if (grounded && Input.GetKeyDown (KeyCode.Space)) {
			anim.SetBool("Ground", false);
			rigidbody2D.AddForce(new Vector2(0, jumpForce));
		}
	}


	void Flip()
	{
		facingRight = !facingRight;
		Vector3 curScale = transform.localScale;
		curScale.x *= -1;

		transform.localScale = curScale;
	}
}
