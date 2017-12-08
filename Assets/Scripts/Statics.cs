﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Statics {

    public static int stageNumber = 1;

	public static int lastClearedStage = (PlayerPrefs.HasKey("lastClearedStage")) ? PlayerPrefs.GetInt("lastClearedStage") : 1;

    public static void updateLastClearedStage() {
		lastClearedStage = PlayerPrefs.GetInt ("lastClearedStage");
    }
}