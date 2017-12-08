using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject musicManager;
    public GameObject player;
    public GameObject canvas;
	public int startingShifts = 5;
    public int numShiftsLeft;

    private AudioSource[] music;
	private bool done = false;

	// Use this for initialization
	void Start () {
		numShiftsLeft = startingShifts;
        music = musicManager.GetComponentsInChildren<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (player.GetComponent<playerController>().isDead && !done) {
			done = true;
			StartCoroutine(PlayGameOver());
        //Reload();
        }

		if (player.GetComponent<playerController>().hasWon && !done) {
			done = true;
			StartCoroutine(PlayClear());
			//Reload();
		}

        //update numShiftsLeft with player shifts
	}

    IEnumerator PlayGameOver() {
        music[0].Stop();
        music[1].Play();
        canvas.GetComponent<Animator>().SetTrigger("PlayerDead");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator PlayClear() {
        music[0].Stop();
        music[2].Play();
        canvas.GetComponent<Animator>().SetTrigger("StageClear");
        yield return new WaitForSeconds(1.5f);
        PlayerPrefs.SetInt("lastClearedStage", Statics.stageNumber);
        Statics.updateLastClearedStage();
        Statics.stageNumber++;
        SceneManager.LoadScene("Stage " + Statics.stageNumber);
    }

    void Reload() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
