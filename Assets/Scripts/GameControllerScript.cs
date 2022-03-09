using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameControllerScript : MonoBehaviour {
    public GameObject bottlePrefab;

    private void generateBottles() {
        GameObject bottle = Instantiate(bottlePrefab);
        bottle.transform.position = new Vector3(-2, 0, 0);
    }

    void Start() {
        generateBottles();
    }

    public void fireShot(PlayerScript player) {
        Debug.Log("Player has fired a shot from position " + player.getPlayerPosition().x);
    }
}
