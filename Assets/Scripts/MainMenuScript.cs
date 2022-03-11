using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuScript : MonoBehaviour {
    private bool player1IsReady;
    private bool player2IsReady;

    void Start() {
        this.player1IsReady = false;
        this.player2IsReady = false;
    }

    private void startGame() {
        SceneManager.LoadScene("GameScene");
    }

    public void player1Ready() {
        this.player1IsReady = true;
        if (this.player2IsReady) {
            this.startGame();
        }
        EventSystem.current.currentSelectedGameObject.SetActive(false);
    }

    public void player2Ready() {
        this.player2IsReady = true;
        if (this.player1IsReady) {
            this.startGame();
        }
        EventSystem.current.currentSelectedGameObject.SetActive(false);
    }
}
