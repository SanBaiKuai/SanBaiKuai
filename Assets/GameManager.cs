using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject musicManager;
    public GameObject player;

    private AudioSource[] music;

	// Use this for initialization
	void Start () {
        music = musicManager.GetComponentsInChildren<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		//if (player.isDead) {
            music[0].Stop();
            music[1].Play();
        // reload current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //}
	}
}
