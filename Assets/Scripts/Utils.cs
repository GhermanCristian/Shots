using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {
    public static int getPlayerIndexFromName(string playerName) {
        if (playerName.EndsWith("1")) {
            return Constants.PLAYER_1_INDEX;
        }
        return Constants.PLAYER_2_INDEX;
    }
}
