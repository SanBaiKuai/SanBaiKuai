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

	// Use this for initialization
	void Start () {
		rb2d = this.GetComponent<Rigidbody2D> ();
		direction = this.transform.localScale;
		currAbility = Abilities.superJump;
	}
	
	// Update is called once per frame
	void Update () {
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
		}
	}

}

