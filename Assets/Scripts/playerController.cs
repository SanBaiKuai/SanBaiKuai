using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

	public float speed = 100;
	public float jump = 250;
	public float superJump = 8000;

	private enum Abilities {superJump, shrink, wallBreak, ghostWalk};

	private Rigidbody2D rb2d;
	private Vector3 direction;
	private bool onGround = true;
	private Abilities currAbility;
	private bool isShrunk = false;

	// Use this for initialization
	void Start () {
		rb2d = this.GetComponent<Rigidbody2D> ();
		currAbility = Abilities.shrink;
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
			
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			executeAbility ();
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
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		//print(onGround);
		if (other.gameObject.CompareTag("floor")){
			//print(onGround);
			onGround = true;
			//anim.SetBool("Contact", true);
		}
		if (other.gameObject.CompareTag("Player")){
			//throwReady = true;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.CompareTag("floor")){
			//print(onGround);
			//anim.SetBool("Contact", false);
			onGround = false;
		}
		if (other.gameObject.CompareTag("Player")){
			//throwReady = false;
		}

	}

}

