using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour {
    public Rigidbody2D rigidBody;

    public GameControllerScript game;
    public int playerWhoFiredIt;
    public const float LOWEST_Y_COORD = -5.7f;

    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void destroy(bool didKill) {
        Destroy(gameObject);
        game.setBombInactive(this.playerWhoFiredIt, didKill);
    }

    void Update() {
        if (transform.position.y > LOWEST_Y_COORD) {
            transform.Translate(0, -0.03f, 0);
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
