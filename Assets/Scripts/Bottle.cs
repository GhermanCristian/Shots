using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour {
    public Rigidbody2D rigidBody;

    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update() {
        
    }

    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("Bottle hit by " + col.name);
        if (col.name.Contains("Bullet")) {
            Destroy(gameObject);
        }
    }
}
