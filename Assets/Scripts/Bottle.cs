using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour {
    public Rigidbody2D rigidBody;
    public GameControllerScript game;
    public GameObject bottle;
    public int points;
    public bool isAlive;
    public float BREAKING_ANIMATION_DURATION_SECONDS;
    public float DESCENDING_SPEED;

    private bool descending;
    private Vector3 destination;

    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        this.isAlive = true;
        this.descending = false;
    }

    public void notifyStartDescending(Vector3 destination) {
        this.descending = true;
        this.destination = destination;
    }

    private IEnumerator descend() {
        while (this.bottle.transform.position != this.destination) {
            this.bottle.transform.position = Vector3.MoveTowards(this.bottle.transform.position, this.destination, DESCENDING_SPEED);
            yield return null;
        }
    }

    void Update() {
        if (this.descending) {
            this.descending = false;
            StartCoroutine(this.descend());
        }
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
