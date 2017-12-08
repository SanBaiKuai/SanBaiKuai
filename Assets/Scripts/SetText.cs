using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SetText : MonoBehaviour {

    public string textToSet;

    private Text text;
    private GameManager gm;
    private playerController pc;

	// Use this for initialization
	void Start () {
        try {
            gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
            if (gm != null) {
                pc = gm.player.GetComponent<playerController>();
            }
        }
        finally {
            text = GetComponent<Text>();
        }
        
	}
	
	// Update is called once per frame
	void Update () {
		switch (textToSet) {
            case "StageSelect":
                text.text = Statics.stageNumber.ToString();
                break;
            case "Stage":
                text.text = SceneManager.GetActiveScene().name;
                break;
            case "Shifts":
                text.text = "Shifts left: " + gm.numShiftsLeft.ToString();
                break;
            case "Ability":
                text.text = "Current ability: " + pc.currAbility;
                break;
            default:
                text.text = "wrong string";
                break;
        }
	}
}
