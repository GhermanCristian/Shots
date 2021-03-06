using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    public GameObject player;
    public GameObject bulletPrefab;
    private Bullet currentBulletClone;
    public Rigidbody2D playerRigidBody;
    public GameObject game;
    public GameControllerScript gameController;
    private int playerNumber;

    public float LEFTMOST_X_COORD;
    public float RIGHTMOST_X_COORD;
    public string PRIMARY_FIRE_INPUT_NAME;
    public string SECONDARY_FIRE_INPUT_NAME;
    public string HORIZONTAL_MOVEMENT_INPUT_NAME;
    public string JUMP_INPUT_NAME;

    private bool isOnTheFloor() {
        return Math.Abs(player.transform.position.y - Constants.LOWEST_Y_COORD_PLAYER) <= 0.2f;
    }

    public Vector3 getPlayerPosition() {
        return player.transform.position;
    }

    private int getPlayerNumber() {
        return int.Parse(player.name.Substring(player.name.Length - 1)) - 1;
    }

    void Start() {
        playerRigidBody = GetComponent<Rigidbody2D>();
        gameController = game.GetComponent<GameControllerScript>();
        this.playerNumber = this.getPlayerNumber();
    }

    private bool isBulletActive() {
        return currentBulletClone != null && currentBulletClone.isAlive == true;
    }

    private void fireShot() {
        if (Input.GetAxis(PRIMARY_FIRE_INPUT_NAME) != 1f || this.isBulletActive()) {
            return;
        }

        this.currentBulletClone = Instantiate(bulletPrefab).GetComponent<Bullet>();
        this.currentBulletClone.player = player;
    }

    private void fireBomb() {
        if (Input.GetAxis(SECONDARY_FIRE_INPUT_NAME) != 1f) {
            return;
        }
        gameController.attemptToFireBomb(this.playerNumber);
    }

    private void moveHorizontally() {
        float horizontalTranslation = Input.GetAxisRaw(HORIZONTAL_MOVEMENT_INPUT_NAME);
        horizontalTranslation *= Time.deltaTime * Constants.PLAYER_HORIZONTAL_SPEED;
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
        float verticalTranslation = Input.GetAxis(JUMP_INPUT_NAME);
        if (verticalTranslation > 0.5f && isOnTheFloor()) {
            playerRigidBody.velocity = Vector3.zero;
            playerRigidBody.AddForce(new Vector2(0, Constants.PLAYER_JUMPING_IMPULSE), ForceMode2D.Impulse);
        }

        // prevent the player from jumping too high or falling through the floor
        if (player.transform.position.y < Constants.LOWEST_Y_COORD_PLAYER) {
            player.transform.position = new Vector3(player.transform.position.x, Constants.LOWEST_Y_COORD_PLAYER, 0);
        }
        if (player.transform.position.y >= Constants.HIGHEST_Y_COORD_PLAYER) {
            player.transform.position = new Vector3(player.transform.position.x, Constants.HIGHEST_Y_COORD_PLAYER, 0);
            playerRigidBody.AddForce(transform.up * -0.7f, ForceMode2D.Impulse);
        }
    }

    void Update() {
        this.moveHorizontally();
        this.jump();
        this.fireShot();
        this.fireBomb();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.name.Contains("FallingBottle") || col.name.Contains("Bomb")) {
            gameController.playerIsDead(this.playerNumber);
            GetComponent<Animator>().SetBool(Constants.PLAYER_DEATH_ANIMATION_KEY, false);
        }
    }
}
