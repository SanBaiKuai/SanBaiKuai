using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour {
	
	public float xMin;
	public float xMax;
	public float yMin;
	public float yMax;

	private Transform player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (player != null) {
			transform.position = new Vector3 (Mathf.Clamp (player.position.x, xMin, xMax), 
				Mathf.Clamp (player.position.y, yMin, yMax), transform.position.z);
		}
	}
}
