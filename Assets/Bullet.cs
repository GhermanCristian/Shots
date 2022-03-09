using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public GameObject player;
    public bool isAlive = true;
    private float HIGHEST_Y_COORD = 6f;

    void Start() {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 0.2f, 0);
    }

    void Update() {
        if (transform.position.y < HIGHEST_Y_COORD) {
            transform.Translate(0, 0.03f, 0);
        }
        else {
            Destroy(gameObject);
            this.isAlive = false;
        }
    }
}
