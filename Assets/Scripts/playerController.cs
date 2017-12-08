using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

	public float speed = 100;
	public float jump = 250;
	public float superJump = 800;

	private enum Abilities {superJump, shrink, wallBreak, ghostWalk, teleport};

	private Rigidbody2D rb2d;
	private Vector3 direction;

	private Abilities currAbility;

	private bool onGround = false;
	private bool isShrunk = false;
	private bool canBreak = false;
	private bool isGhost = false; 
	private bool isTeleport = false;

	private GameObject wallToBeak;

	// Use this for initialization
	void Start () {
		rb2d = this.GetComponent<Rigidbody2D> ();
		currAbility = Abilities.superJump;
	}
	
	// Update is called once per frame
	void Update () {
		direction = this.transform.localScale;
		Vector2 velo = rb2d.velocity;
		//left-right movements
		if (Input.GetKey(KeyCode.W))
		{ if (onGround) {
				velo.y = jump * Time.deltaTime;
				rb2d.velocity = velo;
			}
		}
		if (Input.GetAxis("Horizontal") > 0)
		{
			velo.x = speed*Time.deltaTime;
			rb2d.velocity = velo;
			//anim.SetBool("Walking", true);
			direction.x = Mathf.Abs(direction.x);
			this.transform.localScale = direction;

		}

		if (Input.GetAxis("Horizontal") < 0)
		{
			velo.x = -speed * Time.deltaTime;
			rb2d.velocity = velo;
			//anim.SetBool("Walking", true);
			direction.x = -Mathf.Abs(direction.x);
			this.transform.localScale = direction;

		}
		if (Input.GetAxis("Horizontal") == 0)
		{
			//anim.SetBool("Walking", false);
		}
			
		if (Input.GetKeyDown (KeyCode.E)) {
			executeAbility ();
		}

		if (Input.GetKeyDown (KeyCode.LeftShift)) {
		}
		
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
				StartCoroutine(GhostMode());
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		//print(onGround);
		if (other.gameObject.CompareTag("floor")){
			//print(onGround);
			onGround = true;
			//anim.SetBool("Contact", true);
		}
		if (other.gameObject.CompareTag("WeakWall")){
			canBreak = true;
			wallToBeak = other.gameObject;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.CompareTag("floor")){
			//print(onGround);
			//anim.SetBool("Contact", false);
			onGround = false;
		}
		if (other.gameObject.CompareTag("WeakWall")){
			canBreak = false;
			wallToBeak = null;
			//throwReady = false;
		}
	}

	IEnumerator GhostMode() {
		isGhost = true;
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("KillOnContact");
		foreach (GameObject enemy in enemies) {
			enemy.GetComponent<Collider2D> ().enabled = false;
		}
		//this.gameObject.GetComponent<Renderer> ().material.color.a = 0.5f;
		yield return new WaitForSeconds(5f);
		//this.gameObject.GetComponent<Renderer> ().material.color.a = 1.0f;
		foreach (GameObject enemy in enemies) {
			enemy.GetComponent<Collider2D> ().enabled = true;
		}		
		//cooldown
		yield return new WaitForSeconds (3f);
		isGhost = false;
	}
}

