using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportLocation : MonoBehaviour {

	public float speed = 50;

	private Rigidbody2D rb2d;
	private Vector3 direction;

	// Use this for initialization
	void Start () {
		rb2d = this.GetComponent<Rigidbody2D> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		direction = this.transform.localScale;
		Vector2 velo = rb2d.velocity;
		//left-right movements
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
	}
}
