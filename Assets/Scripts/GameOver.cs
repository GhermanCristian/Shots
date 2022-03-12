using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
    public Text scoreText;

    public void setup(int playerWhoDiedIndex, int[] scores) {
        gameObject.SetActive(true);
        int winnerPlayerIndex = Utils.getOtherPlayerIndex(playerWhoDiedIndex);
        int winnerScore = scores[winnerPlayerIndex];
        int loserScore = scores[playerWhoDiedIndex];
        scoreText.text = string.Format("Player {0} survived with {1} points\nPlayer {2} died :( with {3} points",
                                        Utils.getPlayerNumberFromIndex(winnerPlayerIndex), winnerScore, Utils.getPlayerNumberFromIndex(playerWhoDiedIndex), loserScore);
    }

    public void goBackToMainMenuAction() {
        SceneManager.LoadScene(Constants.MAIN_MENU_SCENE_NAME);
    }
}
