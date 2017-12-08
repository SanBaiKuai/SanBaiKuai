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
        Statics.stageNumber = int.Parse(SceneManager.GetActiveScene().name.Substring(6));
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

	public void displayMessage(string message) {
		canvas.GetComponentsInChildren<SetText> ()[1].sendMessage (message);
	}

	public void displayNotification(string message) {
		canvas.GetComponentsInChildren<SetText> ()[1].sendNotification (message);
	}

	public void displayInstruction(string message) {
		canvas.GetComponentsInChildren<SetText> ()[1].sendInstruction (message);
	}

	public void clearMessage(){
		canvas.GetComponentsInChildren<SetText> ()[1].clearMessage();
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
        yield return new WaitForSeconds(2f);
        PlayerPrefs.SetInt("lastClearedStage", Statics.stageNumber);
        Statics.updateLastClearedStage();
        Statics.stageNumber++;
        SceneManager.LoadScene("Stage " + Statics.stageNumber);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Credits");
    }
}
