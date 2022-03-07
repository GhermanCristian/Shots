using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    public float PLAYER_SPEED = 17.5f;
    public GameObject player;
    public Camera mainCamera;
    public Vector3 LEFTMOST_POSITION;
    public Vector3 RIGHTMOST_POSITION;

    void Update() {
        float translation = Input.GetAxis("Horizontal_P1");
        // the translation should be constant (in this case 0.33 units)
        if (translation > 0.15f) {
            translation = 0.33f;
        }
        else if (translation < -0.15f) {
            translation = -0.33f;
        }
        else { // if the input is small ([-0.15, 0.15] units), the player stops
            translation = 0.0f;
        }

        translation *= Time.deltaTime * PLAYER_SPEED;
        transform.Translate(translation, 0, 0);

        // don't allow the player to go past the screen limits
        if (player.transform.position.x < LEFTMOST_POSITION.x) {
            player.transform.position = LEFTMOST_POSITION;
        }
        if (player.transform.position.x > RIGHTMOST_POSITION.x) {
            player.transform.position = RIGHTMOST_POSITION;
        }
    }
}
