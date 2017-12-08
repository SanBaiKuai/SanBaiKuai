﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

	public float speed = 100;
	public float jump = 250;
	public float superJump = 800;

	public bool isSelecting = false;
	public bool isDead = false;

	public GameObject teleportPrefab;
	public GameObject selectionPrefab;

	public GameObject teleportPoint;
	public GameObject selectionPoint;

	public enum Abilities {superJump, shrink, wallBreak, ghostWalk, teleport};

	private Rigidbody2D rb2d;
	private Vector3 direction;

	public Abilities currAbility;

	private bool onGround = false;
	private bool isShrunk = false;
	private bool canBreak = false;
	private bool isGhost = false; 
	private bool isTeleport = false;

	private GameObject wallToBeak;
	private GameObject newTeleportLocation;
	private GameObject selector;
	Animator anim;

	// Use this for initialization
	void Start () {
		rb2d = this.GetComponent<Rigidbody2D> ();
		currAbility = Abilities.teleport;
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		direction = this.transform.localScale;
		Vector2 velo = rb2d.velocity;
		if(!isSelecting) {
			//left-right movements
			if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
			{ if (onGround) {
					velo.y = jump * Time.deltaTime;
					rb2d.velocity = velo;
				}
			}
			if (Input.GetAxis("Horizontal") > 0)
			{
				velo.x = speed*Time.deltaTime;
				rb2d.velocity = velo;
				anim.SetBool("Walking", true);
				direction.x = Mathf.Abs(direction.x);
				this.transform.localScale = direction;

			}

			if (Input.GetAxis("Horizontal") < 0)
			{
				velo.x = -speed * Time.deltaTime;
				rb2d.velocity = velo;
				anim.SetBool("Walking", true);
				direction.x = -Mathf.Abs(direction.x);
				this.transform.localScale = direction;

			}
			if (Input.GetAxis("Horizontal") == 0)
			{
				anim.SetBool("Walking", false);
			}
				
			if (Input.GetKeyDown (KeyCode.E)) {
				executeAbility ();
			}

			if (Input.GetKeyDown (KeyCode.LeftShift)) {
				enterSelection ();
			}

			if (Input.GetKeyDown (KeyCode.Escape)) {
				clearAllActivities ();
			}
		}
			
	}

	private void enterSelection() {
		clearAllActivities ();
		if (isShrunk || isGhost) {
			return;
		}
		isSelecting = true;
		//create shift ability prefab
		selector = Instantiate(selectionPrefab);
		selector.transform.rotation = this.transform.rotation;
		selector.transform.position = selectionPoint.transform.position;
		//set this as parent of shift ability prefab
		selector.transform.parent = this.transform;
	}

	public void exitSelection() {
		isSelecting = false;
		Destroy (selector);
	}

	private void executeAbility() {
		Vector2 velo = rb2d.velocity;
		if (currAbility == Abilities.superJump) {
			if (onGround) {
				velo.y = superJump * Time.deltaTime;
				rb2d.velocity = velo;
			}
		} else if (currAbility == Abilities.shrink) {
			if (!isShrunk) {
				this.gameObject.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
			} else {
				this.gameObject.transform.localScale = new Vector3 (1f, 1f, 1f);
			}
			isShrunk = !isShrunk;
		} else if (currAbility == Abilities.wallBreak) {
			if (canBreak) {
				wallToBeak.GetComponent<WeakWall> ().breakWall ();
				canBreak = false;
				wallToBeak = null;
			}
		} else if (currAbility == Abilities.ghostWalk) {
			if (!isGhost) {
				StartCoroutine (GhostMode ());
			}
		} else if (currAbility == Abilities.teleport) {
			if (!isTeleport) {
				newTeleportLocation = Instantiate (teleportPrefab);
				newTeleportLocation.transform.rotation = this.transform.rotation;
				newTeleportLocation.transform.position = teleportPoint.transform.position;
				newTeleportLocation.transform.parent = this.transform;
			} else {
				GameObject.Destroy (newTeleportLocation);
				this.transform.position = teleportPoint.transform.position;
			}
			isTeleport = !isTeleport;
		}
	}

	private void clearAllActivities() {
		if (isTeleport) {
			GameObject.Destroy (newTeleportLocation);
			isTeleport = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		//print(onGround);
		if (other.gameObject.CompareTag("floor")){
			//print(onGround);
			onGround = true;
			anim.SetBool("Contact", true);
		}
		if (other.gameObject.CompareTag("WeakWall")){
			canBreak = true;
			wallToBeak = other.gameObject;
		}
		if (other.gameObject.CompareTag ("KillOnContact") && !isGhost) {
			isDead = true;
			anim.Play ("Player_Dying");
			this.enabled = false;
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.CompareTag("floor")){
			//print(onGround);
			onGround = true;
			anim.SetBool("Contact", true);
		}
	}
		
	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.CompareTag("floor")){
			//print(onGround);
			anim.SetBool("Contact", false);
			onGround = false;
		}
		if (other.gameObject.CompareTag("WeakWall")){
			canBreak = false;
			wallToBeak = null;
			//throwReady = false;
		}
			
	}

	IEnumerator GhostMode() {
		Color color = this.gameObject.GetComponent<Renderer> ().material.color;
		isGhost = true;
		Collider2D[] colliders;
		Collider2D[] myColliders = this.gameObject.GetComponents<Collider2D> ();
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("KillOnContact");
		foreach (GameObject enemy in enemies) {
			colliders =enemy.GetComponents<Collider2D> ();
			foreach (Collider2D collider in colliders) {
				foreach (Collider2D myCollider in myColliders) {
					Physics2D.IgnoreCollision (myCollider, collider, true);
				}
			}
		}
		//make player half transparent
		color.a = 0.5f;
		this.gameObject.GetComponent<Renderer> ().material.color = color;
		yield return new WaitForSeconds(5f);
		//make player half opaque
		color.a = 1.0f;
		this.gameObject.GetComponent<Renderer> ().material.color = color;
		foreach (GameObject enemy in enemies) {
			colliders =enemy.GetComponents<Collider2D> ();
			foreach (Collider2D collider in colliders) {
				foreach (Collider2D myCollider in myColliders) {
					Physics2D.IgnoreCollision (myCollider, collider, false);
				}
			}
		}
		//cooldown
		//yield return new WaitForSeconds (3f);
		isGhost = false;
	}
}

