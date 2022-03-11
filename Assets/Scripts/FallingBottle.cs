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
        if (this.isFalling) {
            base.descending = false;
        }
        base.Update();
        if (this.isFalling) {
            if (base.bottle.transform.position.y > Constants.LOWEST_Y_COORD) {
                base.bottle.transform.Translate(0, -Constants.FALLING_BOTTLE_SPEED_MULTIPLIER * base.points, 0);
            }
            else {
                this.isFalling = false;
                Destroy(gameObject);
            }
        }
    }

    private void markAsBroken() {
        base.isAlive = false;
        base.game.bottleWasBroken(base.playerWhoBrokeIt, base.points);
        this.isFalling = true;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.name.Contains("Bullet")) {
            base.playerWhoBrokeIt = col.gameObject.GetComponent<Bullet>().getPlayerName();
            this.markAsBroken();
        }
        else if (col.name.Contains("Player")) {
            Destroy(gameObject);
        }
    }
}
