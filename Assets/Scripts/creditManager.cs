using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class creditManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (Credits ());
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator Credits() {
		yield return new WaitForSeconds(20f);
		SceneManager.LoadScene ("Main Menu");
	}
}
