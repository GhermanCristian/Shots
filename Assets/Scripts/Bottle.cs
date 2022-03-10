using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour {
    public Rigidbody2D rigidBody;
    public GameControllerScript game;
    public int points;
    public bool isAlive;

    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        this.isAlive = false;
    }

    void Update() {
        
    }

    private void destroy() {
        this.isAlive = true;
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.name.Contains("Bullet")) {
            this.destroy();
            game.bottleWasBroken(this);
        }
    }
}
