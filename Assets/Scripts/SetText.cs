using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SetText : MonoBehaviour {

    public string textToSet;

    private Text text;
	private String message;
    private GameManager gm;
    private playerController pc;

	// Use this for initialization
	void Start () {
		message = "";
		gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		pc = gm.player.GetComponent<playerController>();
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
            case "Ability":
                text.text = "Current ability: " + pc.currAbility;
                break;
			case "Message":
				text.text = message;
				break;
            default:
                text.text = "wrong string";
                break;
        }
	}

	public void sendInstruction(String notification) {
		message = notification;
	}
	public void clearMessage(){
		message = "";
	}

	public void sendMessage(String notification) {
		StartCoroutine (DisplayMessage (notification));
	}

	public void sendNotification(String notification) {
		StartCoroutine (DisplayNotification (notification));
	}

	IEnumerator DisplayNotification(String notification) {
		message = notification;
		yield return new WaitForSeconds (4f);
		message = "";
	}

	IEnumerator DisplayMessage(String notification) {
		message = notification;
		text.color = new Color (255f, 255f, 0f);
		yield return new WaitForSeconds(0.2f);
		text.color = new Color (255f, 255f, 255f);
		yield return new WaitForSeconds(0.2f);
		text.color = new Color (255f, 255f, 0f);
		yield return new WaitForSeconds(0.2f);
		text.color = new Color (255f, 255f, 255f);
		yield return new WaitForSeconds(0.2f);
		text.color = new Color (255f, 255f, 0f);
		yield return new WaitForSeconds(0.2f);
		text.color = new Color (255f, 255f, 255f);
		yield return new WaitForSeconds(0.2f);
		text.color = new Color (255f, 255f, 0f);
		yield return new WaitForSeconds(0.2f);
		text.color = new Color (255f, 255f, 255f);
		yield return new WaitForSeconds(0.2f);
		text.color = new Color (255f, 255f, 0f);
		yield return new WaitForSeconds(0.2f);
		text.color = new Color (255f, 255f, 255f);
		yield return new WaitForSeconds(0.2f);
		message = "";
	}
		
}
