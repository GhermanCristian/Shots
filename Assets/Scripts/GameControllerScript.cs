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

    public bool isBomb1Alive;
    public bool isBomb2Alive;
    private const int BOMB_COST = 12;
    public GameObject bombPrefab;

    private System.Random rnd = new System.Random();

    void Start() {
        this.currentScorePlayer1 = 0;
        this.currentScorePlayer2 = 0;
        this.isBomb1Alive = false;
        this.isBomb2Alive = false;
        this.deadPlayer = -1;
        currentScorePlayer1Text.text = "Score: 0";
        currentScorePlayer2Text.text = "Score: 0";
        bottleControllerP1.setPositionAndGenerateInitialBottles(-1);
        bottleControllerP2.setPositionAndGenerateInitialBottles(1);
    }

    private void modifyPoints(ref int playerPoints, Text playerScoreText, int pointDifference) {
        playerPoints += pointDifference;
        playerScoreText.text = string.Format("Score: {0}", playerPoints);
    }

    public void bottleWasBroken(string playerName, int points) {
        if (playerName.EndsWith("1")) {
            this.modifyPoints(ref this.currentScorePlayer1, currentScorePlayer1Text, points);
            this.bottleControllerP1.attemptToShiftDownLastRows();
        }
        else {
            this.modifyPoints(ref this.currentScorePlayer2, currentScorePlayer2Text, points);
            this.bottleControllerP2.attemptToShiftDownLastRows();
        }
    }

    public void playerIsDead(int player) {
        this.deadPlayer = player;
        // TODO - add an animation here
        Invoke("gameOver", 1f);
    }

    public void setBombInactive(int playerNumber, bool didKill) {
        if (playerNumber == 1) {
            this.isBomb1Alive = false;
            if (didKill) {
                playerIsDead(2);
            }
        }
        else {
            this.isBomb2Alive = false;
            if (didKill) {
                playerIsDead(1);
            }
        }
    }

    public void attemptToFireBomb(int playerNumber) {
        if (playerNumber == 1) {
            if (this.isBomb1Alive || this.currentScorePlayer1 < BOMB_COST) {
                return;
            }
            this.isBomb1Alive = true;
            this.modifyPoints(ref this.currentScorePlayer1, currentScorePlayer1Text, -BOMB_COST);
            BombScript bomb = Instantiate(bombPrefab, new Vector3(rnd.Next(1, 11), 6, 0), transform.rotation).GetComponent<BombScript>();
            bomb.game = this;
            bomb.playerWhoFiredIt = 1;
        } 
        else {
            if (this.isBomb2Alive || this.currentScorePlayer2 < BOMB_COST) {
                return;
            }
            this.isBomb2Alive = true;
            this.modifyPoints(ref this.currentScorePlayer2, currentScorePlayer2Text, -BOMB_COST);
            BombScript bomb = Instantiate(bombPrefab, new Vector3(rnd.Next(-11, -1), 6, 0), transform.rotation).GetComponent<BombScript>();
            bomb.game = this;
            bomb.playerWhoFiredIt = 2;
        }
        Debug.Log("player " + playerNumber + " fired a bomb");
    }

    private void gameOver() {
        gameOverScreen.setup(this.deadPlayer, this.currentScorePlayer1, this.currentScorePlayer2);
    }
}
