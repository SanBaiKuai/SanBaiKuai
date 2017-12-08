using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour {

    private bool isPaused = false;
    private Animator anim;

    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!isPaused) {
                Pause();
            } else {
                Unpause();
            }
        }
	}

    public void Pause() {
        StartCoroutine(PauseEnum());
    }

    public void Unpause() {
        StartCoroutine(UnpauseEnum());
    }

    IEnumerator PauseEnum() {
        anim.SetBool("Paused", true);
        yield return new WaitForSeconds(0.1f);
        isPaused = true;
        Time.timeScale = 0f;
        yield return null;
    }

    IEnumerator UnpauseEnum() {
        anim.SetBool("Paused", false);
        Time.timeScale = 1f;
        yield return new WaitForSeconds(0.1f);
        isPaused = false;
        yield return null;
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
}
