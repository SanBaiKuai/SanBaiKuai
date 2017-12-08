using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abilityIconController : MonoBehaviour {

	private SpriteRenderer sr;
	private ShiftAbility sa;

	public Sprite[] Abilities;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer>();
		sa = GetComponentInParent<ShiftAbility>();
		
	}
	
	// Update is called once per frame
	void Update () {
		switch (sa.currSelAbility) {
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
}
