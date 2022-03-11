using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBottle : Bottle {
    private bool isFalling;

    protected override void Start() {
        base.Start();
        this.isFalling = false;
    }

    protected override void Update() {
        base.Update();
        if (this.isFalling) {
            if (base.bottle.transform.position.y > -5.7f) {
                base.bottle.transform.Translate(0, -0.01f * base.points, 0);
            }
            else {
                this.isFalling = false;
                Destroy(gameObject);
            }
        }
    }

    private void markAsBroken() {
        base.isAlive = false;
        base.game.bottleWasBroken(this);
        this.isFalling = true;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.name.Contains("Bullet")) {
            this.markAsBroken();
        }
        else if (col.name.Contains("Player")) {
            Destroy(gameObject);
        }
    }
}
