using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Statics.stageNumber = Mathf.Min(Statics.lastClearedStage + 1, 4);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) {
            StartGame();
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Quit();
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            Decrement();
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.RightArrow)) {
            Increment();
        }
    }

    public void Increment() {
        if (Statics.stageNumber < Statics.lastClearedStage + 1) {
            Statics.stageNumber++;
        }
    }

    public void Decrement() {
        if (Statics.stageNumber > 1) {
            Statics.stageNumber--;
        }
    }

    public void Quit() {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
                          Application.Quit();
#endif
    }

    public void StartGame() {
        SceneManager.LoadScene("Stage " + Statics.stageNumber);
    }

	public void Credits() {
		SceneManager.LoadScene ("Credits");
	}
}
