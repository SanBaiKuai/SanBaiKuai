﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

	public int ghostDuration = 5;

	public float speed = 150;
	public float jump = 250;
	public float superJump = 800;

	public bool isSelecting = false;
	public bool isDead = false;
	public bool hasWon = false;

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
	private bool onCoolDown = false;
    private bool isColliding = false;

	private GameObject wallToBeak;
	private GameObject newTeleportLocation;
	private GameObject selector;
	private abilityDisplayController abilityDisplay;

	private GameManager gm;
	Animator anim;

	// Use this for initialization
	void Awake () {
		rb2d = this.GetComponent<Rigidbody2D> ();
		//currAbility = Abilities.teleport;
		anim = GetComponent<Animator>();
		gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		abilityDisplay = gm.canvas.GetComponentInChildren<abilityDisplayController> ();
	}
	
	// Update is called once per frame
	void Update () {
		direction = this.transform.localScale;
		Vector2 velo = rb2d.velocity;
        isColliding = false;
		if(!isSelecting) {
			//left-right movements
			if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
			{ if (onGround && !isTeleport) {
					velo.y = jump * Time.deltaTime;
					rb2d.velocity = velo;
				} else if (isTeleport) {
					gm.displayMessage ("Can't jump while in Teleport mode");
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
				if (onCoolDown) {
					gm.displayMessage ("Ability on cooldown");
				} else {
					executeAbility ();
				}
			}

			if (Input.GetKeyDown (KeyCode.LeftShift)) {
				if ((gm.numShiftsLeft > 0) && !onCoolDown) {
					enterSelection ();
				} else if (gm.numShiftsLeft <= 0) {
					gm.displayMessage ("You do not have enough shifts left to swap abilities.");
				} else {
					gm.displayMessage ("Ability cannot be swapped while on cooldown");
				}
			}

			if (Input.GetKeyDown (KeyCode.Escape)) {
				clearAllActivities ();
			}
		}
			
	}

	private void enterSelection() {
		anim.SetBool("Walking", false);
		clearAllActivities ();
		if (isShrunk || isGhost) {
			gm.displayMessage ("Ability cannot be swapped while active.");
			return;
		}
		gm.displayNotification ("Press Shift again to select an ability or press Esc to cancel.");
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
				StartCoroutine (CoolDown ());
			}
		} else if (currAbility == Abilities.shrink) {
			if (!isShrunk) {
				abilityDisplay.setActive ();
				this.gameObject.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
			} else {
				this.gameObject.transform.localScale = new Vector3 (2f, 2f, 2f);
				StartCoroutine (CoolDown ());
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
			} else {
				gm.displayMessage ("Ability is already active");
			}
		} else if (currAbility == Abilities.teleport) {
			if (!onGround) {
				gm.displayMessage ("Can't teleport while jumping");
				return;
			}
			if (!isTeleport) {
				gm.displayNotification ("Press E again to teleport to target location or press Esc to cancel.");
				newTeleportLocation = Instantiate (teleportPrefab);
				newTeleportLocation.transform.rotation = this.transform.rotation;
				newTeleportLocation.transform.position = teleportPoint.transform.position;
				newTeleportLocation.transform.parent = this.transform;
			} else {
				if (!newTeleportLocation.GetComponent<teleportController> ().canTeleport) {
					gm.displayMessage ("You cannot teleport there");
					return;
				} 
				GameObject.Destroy (newTeleportLocation);
				this.transform.position = teleportPoint.transform.position;
				StartCoroutine (CoolDown ());
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
			clearAllActivities ();
			isDead = true;
			anim.Play ("Player_Dying");
			this.enabled = false;
		}
        if (other.gameObject.CompareTag("PieceOfShift")) {
            if (isColliding) {
                return;
            }
            isColliding = true;
            Destroy(other.gameObject);
			gm.displayNotification ("You found a Piece of Shiftonite!\n +2 Shifts");
            gm.numShiftsLeft += 2;
        }
		if (other.gameObject.CompareTag("Finish")){
			anim.SetBool("Walking", false);
			hasWon = true;
			other.gameObject.GetComponent<Animator> ().SetTrigger ("Touched");
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

	IEnumerator CoolDown() {
		onCoolDown = true;
		abilityDisplay.waitCountDown (2);
		yield return new WaitForSeconds(2f);
		onCoolDown = false;
	}
		

	IEnumerator GhostMode() {
		abilityDisplay.activeCountDown (5);
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
		abilityDisplay.setInactive ();
		yield return new WaitForSeconds(0.1f);
		StartCoroutine (CoolDown ());
	}
}

