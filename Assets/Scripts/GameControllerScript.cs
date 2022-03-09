using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameControllerScript : MonoBehaviour {
    public GameObject bottlePrefab;

    private void generateBottles() {
        for (int row = 1; row <= 3; row++) {
            int column;
            if (row % 2 == 1) {
                column = 1;
            }
            else {
                column = 2;
            }
            for (; column <= 11; column += 2) {
                GameObject bottle = Instantiate(bottlePrefab);
                bottle.transform.position = new Vector3(-column + 0.5f, 2 * row - 1, 0);
            }
        }
        
    }

    void Start() {
        generateBottles();
    }

    public void fireShot(PlayerScript player) {
        Debug.Log("Player has fired a shot from position " + player.getPlayerPosition().x);
    }
}
