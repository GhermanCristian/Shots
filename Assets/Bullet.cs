using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public GameObject player;
    public bool isAlive = true;

    void Start() {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 0.2f, 0);
    }

    void Update() {
        if (transform.position.y < 4.6f) {
            transform.Translate(0, 0.03f, 0);
        }
        else {
            Destroy(gameObject);
            this.isAlive = false;
        }
    }
}
