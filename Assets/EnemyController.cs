using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float speed = 5f;
    private Animator anim;
    private Rigidbody2D rb2D;
    private bool isDead;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        StartCoroutine(Movement());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Movement() {
        while (true) {
            yield return new WaitForSeconds(1f);
            rb2D.velocity = new Vector2(speed * -1f, 0);
            yield return new WaitForSeconds(1f);
            rb2D.velocity = Vector2.zero;
            yield return new WaitForSeconds(1f);
            rb2D.velocity = new Vector2(speed * 1f, 0);
            yield return new WaitForSeconds(1f);
            rb2D.velocity = Vector2.zero;
        }
    }

    void FixedUpdate() {
        if (!isDead) {
            if (Mathf.Abs(rb2D.velocity.x) > 0.1f) {
                anim.SetBool("walking", true);
            }
            else {
                anim.SetBool("walking", false);
            }

            if (rb2D.velocity.x < -0.1f) {
                Vector3 scaleTmp = transform.localScale;
                scaleTmp.x = 1;
                transform.localScale = scaleTmp;
            }
            else if (rb2D.velocity.x > 0.1f) {
                Vector3 scaleTmp = transform.localScale;
                scaleTmp.x = -1;
                transform.localScale = scaleTmp;
            }
        }
    }
}
