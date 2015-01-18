using UnityEngine;
using System.Collections;

public class RobotController : MonoBehaviour {

	public float maxSpeed = 10.0f;
	bool facingRight = true;

	Animator anim;
	bool grounded = false;
	float groundRadius = 0.2f;
	float ceilingRadius = 0.2f;

	public Transform groundCheck;
	public Transform ceilingCheck;
	public LayerMask whatIsGround;
	public float jumpForce = 700f;

	[Range(0, 1)] [SerializeField] 
	private float crouchSpeed = .36f;
	// Amount of maxSpeed applied to crouching movement. 1 = 100%
	
	[SerializeField] 
	private bool airControl = false; // Whether or not a player can steer while jumping;

	private Collider2D standCollider;
	private Collider2D crouchCollider;
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}

	void Awake()
	{
		Collider2D[] c = GetComponentsInChildren<Collider2D> ();
		Debug.Log ("colliders: " + c.Length);
		foreach (Collider2D d in c)
		{
			Debug.Log("collider: " + d.name+ " :" + d.enabled);
		}

		Transform standBox = transform.FindChild("standBox");
		if (standBox) {
			Debug.Log("found box collider: "+standBox.gameObject.collider2D.name);		
			standCollider = standBox.gameObject.collider2D;
		}

		Transform crouchdBox = transform.FindChild("crouchBox");
		if (crouchdBox) {
			Debug.Log("found box collider: "+crouchdBox.gameObject.collider2D.name);		
			crouchCollider = crouchdBox.gameObject.collider2D;
		}
		
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		// Read the inputs.
		bool crouch = Input.GetKey(KeyCode.LeftControl);

		// If crouching, check to see if the character can stand up
		if (!crouch && anim.GetBool("Crouch"))
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatIsGround))
				crouch = true;
		}


		standCollider.enabled = !crouch;
		crouchCollider.enabled = crouch;
		
		// Set whether or not the character is crouching in the animator
		anim.SetBool("Crouch", crouch);
		
		
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);

		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);

		if (grounded || airControl) {
			float move = Input.GetAxis("Horizontal");

			move = crouch ? crouchSpeed * move : move;
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
