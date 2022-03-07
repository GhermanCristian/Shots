using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    public GameObject player;
    public Rigidbody2D playerRigidBody;

    public float LEFTMOST_X_COORD;
    public float RIGHTMOST_X_COORD;
    public const float PLAYER_HORIZONTAL_SPEED = 17.5f;
    public const float LOWEST_Y_COORD = -4.4f;
    public const float HIGHEST_Y_COORD = -0.5f;

    void Start() {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update() {
        float horizontalTranslation = Input.GetAxis("Horizontal_P1");
        float verticalTranslation = Input.GetAxis("Jump_P1");

        // the translation should be constant (in this case 0.33 units)
        if (horizontalTranslation > 0.15f) {
            horizontalTranslation = 0.33f;
        }
        else if (horizontalTranslation < -0.15f) {
            horizontalTranslation = -0.33f;
        }
        else { // if the input is small ([-0.15, 0.15] units), the player stops
            horizontalTranslation = 0.0f;
        }

        // jump
        if (verticalTranslation > 0.5f && player.transform.position.y < HIGHEST_Y_COORD) {
            // TODO - prevent double-jumping
            playerRigidBody.AddForce(transform.up * 3f, ForceMode2D.Impulse);
        }

        horizontalTranslation *= Time.deltaTime * PLAYER_HORIZONTAL_SPEED;
        transform.Translate(horizontalTranslation, 0, 0);

        // don't allow the player to go past the screen limits
        if (player.transform.position.x < LEFTMOST_X_COORD) {
            player.transform.position = new Vector3(LEFTMOST_X_COORD, player.transform.position.y, 0);
        }
        if (player.transform.position.x > RIGHTMOST_X_COORD) {
            player.transform.position = new Vector3(RIGHTMOST_X_COORD, player.transform.position.y, 0);
        }
        if (player.transform.position.y < LOWEST_Y_COORD) {
            player.transform.position = new Vector3(player.transform.position.x, LOWEST_Y_COORD, 0);
        }
        if (player.transform.position.y >= HIGHEST_Y_COORD) {
            player.transform.position = new Vector3(player.transform.position.x, HIGHEST_Y_COORD, 0);
            playerRigidBody.AddForce(transform.up * -0.7f, ForceMode2D.Impulse);
        }
    }
}
