using UnityEngine;
using System.Collections;

public class CubeTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {

		//Debug.Log("OnTriggerEnter2D called!!!!!" + other.gameObject.name);
		if (other.gameObject.name == "circle") {
			//Application.LoadLevel("TestScene1");
		}
	}
}
