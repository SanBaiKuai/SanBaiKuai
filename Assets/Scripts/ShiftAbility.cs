using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftAbility : MonoBehaviour {

	public playerController.Abilities currSelAbility;

	private playerController.Abilities tempAbility;
    private TextMesh text;
    private playerController pc;
    private Transform parentTransform;
    private static int numAbilities = 5;

	private GameManager gm;
	// Use this for initialization
	void Start () {
        text = GetComponent<TextMesh>();
        pc = GetComponentInParent<playerController>();
        parentTransform = GetComponentInParent<Transform>();
		tempAbility = pc.currAbility;
		gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = tempAbility.ToString();
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
			if (tempAbility <= 0) {
				tempAbility = (playerController.Abilities)(numAbilities - 1);
            }
            else {
				tempAbility = (playerController.Abilities)(((int)tempAbility - 1));
            }
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
			tempAbility = (playerController.Abilities)(((int)tempAbility + 1) % numAbilities);
        }

		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			if (pc.currAbility != tempAbility) {
				gm.numShiftsLeft--;
			}
			pc.currAbility = tempAbility;
			exitSelection ();
			
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			//pc.currAbility = tempAbility;
			exitSelection ();
		}
		//currSelAbility = pc.currAbility;
		currSelAbility = tempAbility;
        Vector3 scaleTmp = transform.localScale;
        scaleTmp.x /= parentTransform.lossyScale.x;
        transform.localScale = scaleTmp;
    }

	private void exitSelection() {
		pc.exitSelection ();
	}
}
