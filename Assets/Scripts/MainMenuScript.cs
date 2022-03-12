using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuScript : MonoBehaviour {
    private bool[] playerIsReady = { false, false };

    private void startGame() {
        SceneManager.LoadScene(Constants.GAME_SCENE_NAME);
    }

    private void playerReady(int playerIndex) {
        this.playerIsReady[playerIndex] = true;
        if (this.playerIsReady[Utils.getOtherPlayerIndex(playerIndex)]) {
            this.startGame();
        }
        EventSystem.current.currentSelectedGameObject.SetActive(false);
    }

    public void player1Ready() {
        this.playerReady(Constants.PLAYER_1_INDEX);
    }

    public void player2Ready() {
        this.playerReady(Constants.PLAYER_2_INDEX);
    }

    public void quitGame() {
        Debug.Log("after quit");
        Application.Quit();
        Debug.Log("after quit");
    }
}
