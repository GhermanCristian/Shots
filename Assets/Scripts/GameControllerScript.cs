using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class GameControllerScript : MonoBehaviour {
    public Text[] currentScoreText;
    public GameObject[] bottlePrefabs;
    public BottleController[] bottleControllers;
    public GameObject bombPrefab;
    public GameOver gameOverScreen;

    private int[] currentScore = { 0, 0 };
    private int deadPlayerIndex;    
    public bool[] isBombAlive = { false, false };
    private System.Random rnd = new System.Random();

    void Start() {
        currentScoreText[Constants.PLAYER_1_INDEX].text = Constants.DEFAULT_SCORE_TEXT;
        currentScoreText[Constants.PLAYER_2_INDEX].text = Constants.DEFAULT_SCORE_TEXT;
        bottleControllers[Constants.PLAYER_1_INDEX].setPositionAndGenerateInitialBottles(Constants.PLAYER_1_POSITION);
        bottleControllers[Constants.PLAYER_2_INDEX].setPositionAndGenerateInitialBottles(Constants.PLAYER_2_POSITION);
    }

    private void modifyPoints(int playerIndex, int pointDifference) {
        this.currentScore[playerIndex] += pointDifference;
        currentScoreText[playerIndex].text = string.Format("Score: {0}", this.currentScore[playerIndex]);
    }

    public void bottleWasBroken(string playerName, int points) {
        int playerIndex = Utils.getPlayerIndexFromName(playerName);
        this.modifyPoints(playerIndex, points);
        this.bottleControllers[playerIndex].attemptToShiftDownLastRows();
    }

    public void playerIsDead(int playerIndex) {
        this.deadPlayerIndex = playerIndex;
        Invoke("gameOver", 0.833f);
    }

    public void setBombInactive(int playerIndex) {
        this.isBombAlive[playerIndex] = false;
    }

    public void attemptToFireBomb(int playerIndex) {
        if (this.isBombAlive[playerIndex] || this.currentScore[playerIndex] < Constants.BOMB_COST) {
            return;
        }
        int LEFTMOST_X_COORD, RIGHTMOST_X_COORD;
        if (playerIndex == Constants.PLAYER_1_INDEX) {
            LEFTMOST_X_COORD = 1;
            RIGHTMOST_X_COORD = 11;
        }
        else {
            LEFTMOST_X_COORD = -11;
            RIGHTMOST_X_COORD = -1;
        }

        this.isBombAlive[playerIndex] = true;
        this.modifyPoints(playerIndex, -Constants.BOMB_COST);
        BombScript bomb = Instantiate(bombPrefab, new Vector3(rnd.Next(LEFTMOST_X_COORD, RIGHTMOST_X_COORD), Constants.HIGHEST_Y_COORD, 0), transform.rotation).GetComponent<BombScript>();
        bomb.game = this;
        bomb.playerWhoFiredIt = playerIndex;
    }

    private void gameOver() {
        gameOverScreen.setup(this.deadPlayerIndex, this.currentScore);
    }
}
