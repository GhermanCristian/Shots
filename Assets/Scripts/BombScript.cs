using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour {
    public Rigidbody2D rigidBody;
    public GameControllerScript game;
    public int playerWhoFiredIt;

    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void destroy(bool didKill) {
        Destroy(gameObject);
        game.setBombInactive(this.playerWhoFiredIt, didKill);
    }

    void Update() {
        if (transform.position.y > Constants.LOWEST_Y_COORD) {
            transform.Translate(0, -Constants.BOMB_SPEED, 0);
        }
        else {
            this.destroy(false);
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.name.Contains("Player")) {
            this.destroy(true);
        }
    }
}
