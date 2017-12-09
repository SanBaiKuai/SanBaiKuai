using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour {
	
	public float xMin;
	public float xMax;
	public float yMin;
	public float yMax;
	public float speed = 10f;

	private bool isPlanning = true;

	private GameObject player;
	private Transform playerTransform;
	private GameManager gm;

	// Use this for initialization
	void Awake () {
		player = GameObject.Find ("Player");
		player.GetComponent<playerController> ().enabled = false;
		playerTransform = GameObject.Find ("Player").transform;
		gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

	}
	
	// Update is called once per frame
	void Update () {
		if (isPlanning) {
            gm.displayInstruction("You are now in planning mode, use arrow keys or wasd to take a look at the map.\nPress Esc when you are done.");
            if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
				transform.position = new Vector3 (Mathf.Clamp (transform.position.x + (speed*Time.deltaTime), xMin, xMax), transform.position.y, transform.position.z);
			}

			if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
				transform.position = new Vector3 (Mathf.Clamp (transform.position.x - (speed*Time.deltaTime), xMin, xMax), transform.position.y, transform.position.z);
			}

			if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {
				transform.position = new Vector3 (transform.position.x, Mathf.Clamp (transform.position.y + (speed*Time.deltaTime), yMin, yMax), transform.position.z);
			}

			if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) {
				transform.position = new Vector3 (transform.position.x, Mathf.Clamp (transform.position.y - (speed*Time.deltaTime), yMin, yMax), transform.position.z);
			}

			if (Input.GetKeyDown (KeyCode.Escape)) {
				isPlanning = false;
				player.GetComponent<playerController> ().enabled = true;
				gm.clearMessage ();
			}
		} else {
			if (playerTransform != null) {
				transform.position = new Vector3 (Mathf.Clamp (playerTransform.position.x, xMin, xMax), 
					Mathf.Clamp (playerTransform.position.y, yMin, yMax), transform.position.z);
			}
		}
			
	}
}
