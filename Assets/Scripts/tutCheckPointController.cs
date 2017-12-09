using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutCheckPointController : MonoBehaviour {

	public string tutMessage;
	private GameManager gm;
	private bool hasBeenTriggered;

	// Use this for initialization
	void Start () {
		hasBeenTriggered = false;
		gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player") && !hasBeenTriggered) {
			hasBeenTriggered = true;
			gm.displayInstruction (tutMessage);
		}
	}
}
