using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour {
    public Rigidbody2D rigidBody;
    public GameControllerScript game;
    public int points;
    public bool isAlive;
    public float BREAKING_ANIMATION_DURATION_SECONDS;

    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        this.isAlive = true;
    }

    void Update() {
        
    }

    private void destroy() {
        this.isAlive = false;
        game.bottleWasBroken(this);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.name.Contains("Bullet")) {
            GetComponent<Animator>().SetBool("isAlive", false);
            // wait before initiating the destroy process, such that the animation has an object to be done on
            Invoke("destroy", BREAKING_ANIMATION_DURATION_SECONDS);
        }
    }
}
