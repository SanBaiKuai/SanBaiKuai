using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SetText : MonoBehaviour {

    public string textToSet;

    private Text text;
    private GameManager gm;

	// Use this for initialization
	void Start () {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		switch (textToSet) {
            case "Stage":
                text.text = SceneManager.GetActiveScene().name;
                break;
            case "Shifts":
                text.text = "Shifts left: " + gm.numShiftsLeft.ToString();
                break;
            default:
                text.text = "wrong string";
                break;
        }
	}
}
