using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
    public Text scoreText;

    public void setup(int playerWhoDiedIndex, int[] scores) {
        gameObject.SetActive(true);
        int winnerScore = scores[1 - playerWhoDiedIndex];
        int loserScore = scores[playerWhoDiedIndex];
        scoreText.text = string.Format("Player {0} survived with {1} points\nPlayer {2} died :( with {3} points", 
                                        2 - playerWhoDiedIndex, winnerScore, playerWhoDiedIndex + 1, loserScore);
    }

    public void goBackToMainMenuAction() {
        SceneManager.LoadScene("MainMenuScene");
    }
}
