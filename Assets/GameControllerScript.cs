using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameControllerScript {
    private static GameControllerScript instance;

    public static GameControllerScript getInstance() {
        if (instance == null) {
            instance = new GameControllerScript();
        }
        return instance;
    }

    public void fireShot(PlayerScript player) {
        Debug.Log("Player has fired a shot from position " + player.getPlayerPosition().x);
    }
}
