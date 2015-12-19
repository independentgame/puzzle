using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

	private Animator anim;
	private RobotController rc;
	// Use this for initialization
	void Start () {
		anim = GetComponentInParent<Animator> ();
		if (anim != null) {
			Debug.Log("get animator successfully!");		
		}

		rc = GetComponentInParent<RobotController> ();
		if (rc != null) {
			Debug.Log("robot controller!");	
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		//Debug.Log("this is a test idea!!!!!!!");

		if (other.gameObject.name == "CubeTestScene1") {
			GameObject.DontDestroyOnLoad(transform.parent.gameObject);
			Application.LoadLevel("TestScene1");
		}

		if (other.gameObject.name == "CubeRobotScene") {
			GameObject.DontDestroyOnLoad(transform.parent.gameObject);
			Application.LoadLevel("RobotScene");
		}

		if (other.gameObject.name == "Box") {
			Debug.Log("box trigger entered!!!!!!!" + transform.parent.rigidbody2D.velocity.x);
			rc.isGoingToPushBox = transform.parent.rigidbody2D.velocity.x < 10;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.name == "Box") {
			Debug.Log("box trigger exit!!!!!!!");	
			rc.isGoingToPushBox = false;
		}
	}
	
	void OnCollisionEnter2D(Collision2D coll) {

		//Debug.Log("this is a test collision!!!!!!!  " + coll.gameObject.tag);
		if (coll.gameObject.tag == "Enemy")
			coll.gameObject.SendMessage("ApplyDamage", 10);
			
	}
}
