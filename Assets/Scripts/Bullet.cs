using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public GameObject player;
    public Rigidbody2D rigidBody;
    public bool isAlive = true;
    private float HIGHEST_Y_COORD = 6f;

    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 0.2f, 0);
    }

    private void destroy() {
        Destroy(gameObject);
        this.isAlive = false;
    }

    void Update() {
        if (transform.position.y < HIGHEST_Y_COORD) {
            transform.Translate(0, 0.06f, 0);
        }
        else {
            this.destroy();
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("Bullet has hit a " + col.name);
        this.destroy();
    }
}
