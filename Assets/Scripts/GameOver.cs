using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
    public Text scoreText;

    public void setup(int playerWhoDied, int scorePlayer1, int scorePlayer2) {
        gameObject.SetActive(true);
        int winnerScore, loserScore;
        if (playerWhoDied == 1) {
            winnerScore = scorePlayer1;
            loserScore = scorePlayer2;
        }
        else {
            winnerScore = scorePlayer2;
            loserScore = scorePlayer1;
        }
        scoreText.text = string.Format("Player{0} won with {1} points\nPlayer{2} died :( with {3} points", 
                                        3 - playerWhoDied, winnerScore, playerWhoDied, loserScore);
    }

    public void continueButtonAction() {
        SceneManager.LoadScene("GameScene"); // reload the game
    }
}
