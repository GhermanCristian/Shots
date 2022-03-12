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

    protected bool descending;
    private Vector3 destination;

    protected string playerWhoBrokeIt;

    protected virtual void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        this.isAlive = true;
        this.descending = false;
    }

    public void notifyStartDescending(Vector3 destination) {
        this.descending = true;
        this.destination = destination;
    }

    protected IEnumerator descend() {
        while (this.bottle.transform.position != this.destination) {
            this.bottle.transform.position = Vector3.MoveTowards(this.bottle.transform.position, this.destination, DESCENDING_SPEED);
            yield return null;
        }
    }

    protected virtual void Update() {
        if (this.descending) {
            this.descending = false;
            StartCoroutine(this.descend());
        }
    }

    private void destroy() {
        this.isAlive = false;
        game.bottleWasBroken(this.playerWhoBrokeIt, this.points);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.name.Contains("Bullet")) {
            this.playerWhoBrokeIt = col.gameObject.GetComponent<Bullet>().getPlayerName();
            GetComponent<Animator>().SetBool(Constants.DESTROYED_BOTTLE_ANIMATION_KEY, false);
            // wait before initiating the destroy process, such that the animation has an object to be done on
            Invoke("destroy", BREAKING_ANIMATION_DURATION_SECONDS);
        }
    }
}
