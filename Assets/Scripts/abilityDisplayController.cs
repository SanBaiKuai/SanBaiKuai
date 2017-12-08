using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class abilityDisplayController : MonoBehaviour {

	public Sprite[] Abilities;

	private Image sr;
	private Image im;
	private GameManager gm;
	private playerController pc;
	private Text text;

	// Use this for initialization
	void Start () {
		text = GetComponentInChildren<Text> ();
		text.text = "";
		im = GetComponentsInChildren<Image> () [0];
		sr = GetComponentsInChildren<Image>()[1];
		gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		pc = gm.player.GetComponent<playerController>();
		
	}
	public void setActive() {
		im.color = Color.green;
	}

	public void setInactive(){
		im.color = Color.gray;
	}
		
	public void activeCountDown(int time) {
		StartCoroutine (active (time));
	}

	public void waitCountDown(int time) {
		StartCoroutine (wait (time));
	}
	// Update is called once per frame
	void Update () {
		switch (pc.currAbility) {
		case playerController.Abilities.ghostWalk:
			sr.sprite = Abilities [0];
			break;
		case playerController.Abilities.shrink:
			sr.sprite = Abilities [1];
			break;
		case playerController.Abilities.superJump:
			sr.sprite = Abilities [2];
			break;
		case playerController.Abilities.wallBreak:
			sr.sprite = Abilities [3];
			break;
		case playerController.Abilities.teleport:
			sr.sprite = Abilities [4];
			break;
		}
	}

	IEnumerator active(int waitTime) {
		im.color = Color.green;
		while(waitTime > 0) {
			text.text = waitTime.ToString();
			yield return new WaitForSeconds(1f);
			waitTime--;
		}
		text.text = "";
		im.color = Color.white;
	}

	IEnumerator wait(int waitTime) {
		im.color = Color.gray;
		while(waitTime > 0) {
			text.text = waitTime.ToString();
			yield return new WaitForSeconds(1f);
			waitTime--;
		}
		text.text = "";
		im.color = Color.white;
	}
}
