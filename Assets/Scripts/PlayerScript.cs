using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    public GameObject player;
    public GameObject bulletPrefab;
    private Bullet currentBulletClone;
    public Rigidbody2D playerRigidBody;
    public GameObject gameController;
    private GameControllerScript gameControllerScript;

    public float LEFTMOST_X_COORD;
    public float RIGHTMOST_X_COORD;
    public const float PLAYER_HORIZONTAL_SPEED = 7f;
    public const float LOWEST_Y_COORD = -5.7f;
    public const float HIGHEST_Y_COORD = -0.5f;

    private bool isOnTheFloor() {
        return Math.Abs(player.transform.position.y - LOWEST_Y_COORD) <= 0.2f;
    }

    public Vector3 getPlayerPosition() {
        return player.transform.position;
    }

    void Start() {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    private bool isBulletActive() {
        return currentBulletClone != null && currentBulletClone.isAlive == true;
    }

    private void fireShot() {
        if (Input.GetAxis("Fire1_P1") != 1f || this.isBulletActive()) {
            return;
        }

        this.currentBulletClone = Instantiate(bulletPrefab).GetComponent<Bullet>();
        this.currentBulletClone.player = player;
    }

    private void moveHorizontally() {
        float horizontalTranslation = Input.GetAxisRaw("Horizontal_P1");
        horizontalTranslation *= Time.deltaTime * PLAYER_HORIZONTAL_SPEED;
        transform.Translate(horizontalTranslation, 0, 0);

        // prevent the player from going past the screen limits
        if (player.transform.position.x < LEFTMOST_X_COORD) {
            player.transform.position = new Vector3(LEFTMOST_X_COORD, player.transform.position.y, 0);
        }
        if (player.transform.position.x > RIGHTMOST_X_COORD) {
            player.transform.position = new Vector3(RIGHTMOST_X_COORD, player.transform.position.y, 0);
        }
    }

    private void jump() {
        float verticalTranslation = Input.GetAxis("Jump_P1");
        if (verticalTranslation > 0.5f && isOnTheFloor()) {
            playerRigidBody.velocity = Vector3.zero;
            playerRigidBody.AddForce(new Vector2(0, 280f), ForceMode2D.Impulse);
        }

        // prevent the player from jumping too high or falling through the floor
        if (player.transform.position.y < LOWEST_Y_COORD) {
            player.transform.position = new Vector3(player.transform.position.x, LOWEST_Y_COORD, 0);
        }
        if (player.transform.position.y >= HIGHEST_Y_COORD) {
            player.transform.position = new Vector3(player.transform.position.x, HIGHEST_Y_COORD, 0);
            playerRigidBody.AddForce(transform.up * -0.7f, ForceMode2D.Impulse);
        }
    }

    void Update() {
        this.moveHorizontally();
        this.jump();
        this.fireShot();
    }
}
