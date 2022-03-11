using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public GameObject player;
    public Rigidbody2D rigidBody;
    public bool isAlive = true;

    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.6f, 0);
    }

    private void destroy() {
        Destroy(gameObject);
        this.isAlive = false;
    }

    public string getPlayerName() {
        return this.player.name;
    }

    void Update() {
        if (transform.position.y < Constants.HIGHEST_Y_COORD) {
            transform.Translate(0, Constants.BULLET_SPEED, 0);
        }
        else {
            this.destroy();
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.name.Contains("Bottle")) {
            this.destroy();
        }
    }
}
