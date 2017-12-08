using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject musicManager;
    public GameObject player;
    public int numShiftsLeft = 5;

    private AudioSource[] music;

	// Use this for initialization
	void Start () {
        music = musicManager.GetComponentsInChildren<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        //if (player.isDead) {
        //PlayGameOver();
        //Reload();
        //}

        //update numShiftsLeft with player shifts
	}

    void PlayGameOver() {
        music[0].Stop();
        music[1].Play();
    }

    void Reload() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
