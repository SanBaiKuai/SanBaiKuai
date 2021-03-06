﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakWall : MonoBehaviour {

    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void breakWall(){
		StartCoroutine(Break());
	}

//    void OnTriggerEnter2D(Collider2D other) {
//        if (other.tag == "Wallbreaker") {
//            StartCoroutine(Break());
//        }
//    }

    IEnumerator Break() {
        anim.SetTrigger("Break");
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
