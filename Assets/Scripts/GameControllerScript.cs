using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class GameControllerScript : MonoBehaviour {
    public Text currentScorePlayer1Text;
    public Text currentScorePlayer2Text;

    private int currentScorePlayer1;
    private int currentScorePlayer2;
    private int deadPlayer;

    public GameObject[] bottlePrefabs;
    public GameOver gameOverScreen;

    public BottleController bottleControllerP1;
    public BottleController bottleControllerP2;

    void Start() {
        this.currentScorePlayer1 = 0;
        this.currentScorePlayer2 = 0;
        this.deadPlayer = -1;
        currentScorePlayer1Text.text = "Score: 0";
        currentScorePlayer2Text.text = "Score: 0";
        bottleControllerP1.setPositionAndGenerateInitialBottles(-1);
        bottleControllerP2.setPositionAndGenerateInitialBottles(1);
    }

    public void bottleWasBroken(string playerName, int points) {
        if (playerName.EndsWith("1")) {
            this.currentScorePlayer1 += points;
            currentScorePlayer1Text.text = string.Format("Score: {0}", this.currentScorePlayer1);
            this.bottleControllerP1.attemptToShiftDownLastRows();
        }
        else {
            this.currentScorePlayer2 += points;
            currentScorePlayer2Text.text = string.Format("Score: {0}", this.currentScorePlayer2);
            this.bottleControllerP2.attemptToShiftDownLastRows();
        }
    }

    public void playerIsDead(int player) {
        this.deadPlayer = player;
        // TODO - add an animation here
        Invoke("gameOver", 1f);
    }

    private void gameOver() {
        gameOverScreen.setup(this.deadPlayer, this.currentScorePlayer1, this.currentScorePlayer2);
    }
}
