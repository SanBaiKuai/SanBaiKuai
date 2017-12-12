using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Statics {

    public static int stageNumber = 1;
    public static int volume = 5;

	public static int lastClearedStage = PlayerPrefs.GetInt("lastClearedStage");

    public static void updateLastClearedStage() {
		lastClearedStage = PlayerPrefs.GetInt ("lastClearedStage");
    }
}
