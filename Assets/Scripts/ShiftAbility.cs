using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftAbility : MonoBehaviour {

    private TextMesh text;
    private playerController pc;
    private Transform parentTransform;
    private static int numAbilities = 5;

	// Use this for initialization
	void Start () {
        text = GetComponent<TextMesh>();
        pc = GetComponentInParent<playerController>();
        parentTransform = GetComponentInParent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = pc.currAbility.ToString();
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (pc.currAbility <= 0) {
                pc.currAbility = (playerController.Abilities)(numAbilities - 1);
            }
            else {
                pc.currAbility = (playerController.Abilities)(((int)pc.currAbility - 1));
            }
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            pc.currAbility = (playerController.Abilities)(((int)pc.currAbility + 1) % numAbilities);
        }

        Vector3 scaleTmp = transform.localScale;
        scaleTmp.x /= parentTransform.lossyScale.x;
        transform.localScale = scaleTmp;
    }
}
