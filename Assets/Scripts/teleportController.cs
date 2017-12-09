using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportController : MonoBehaviour {

	public bool canTeleport = true; 
	private int numCollisions;

	// Use this for initialization
	void Start () {
		numCollisions = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		canTeleport = false;
		this.GetComponent<SpriteRenderer> ().color = Color.red;
		numCollisions++;
		Debug.Log (numCollisions);
	}

	void OnTriggerExit2D(Collider2D other) {
		numCollisions--;
		if (numCollisions == 0) {
			canTeleport = true;
			this.GetComponent<SpriteRenderer> ().color = Color.white;
		}
		Debug.Log (numCollisions);
	}
}
