using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class GameControllerScript : MonoBehaviour {
    private const int ROW_COUNT = 3;
    private const int COLUMN_COUNT = 11;
    private const int LAST_ROW_INDEX = 0; // the lowest row in the game view

    public GameObject[] bottlePrefabs;
    public Text currentScorePlayer1Text;
    private GameObject[,] bottles = new GameObject[ROW_COUNT, COLUMN_COUNT];
    private int totalBottleRows;
    private int currentScorePlayer1;

    private System.Random rnd = new System.Random(); // add the "System" namespace to distinguish from the Unity Random class

    public GameOver gameOverScreen;

    private void generateInitialBottles() {
        while (this.totalBottleRows < ROW_COUNT) {
            this.addNewRow();
        }
    }

    private Vector3 computePositionForBottle(int row, int column) {
        return new Vector3(-column - 0.5f, 2 * row + 1, 0);
    }

    void Start() {
        this.totalBottleRows = 0;
        this.currentScorePlayer1 = 0;
        currentScorePlayer1Text.text = "Score: 0";
        generateInitialBottles();
    }

    private bool isLastRowDestroyed() {
        for (int column = 0; column < COLUMN_COUNT; column++) {
            if (bottles[LAST_ROW_INDEX, column] != null) {
                if (bottles[LAST_ROW_INDEX, column].GetComponent<Bottle>().isAlive == true) {
                    return false;
                }
            }
        }
        return true;
    }

    private void shiftRowsDown() {
        // "down" in the view, but in the matrix it's actually a move up
        for (int tempRow = 0; tempRow < 2; tempRow++) {
            for (int column = 0; column < COLUMN_COUNT; column++) {
                bottles[tempRow, column] = bottles[tempRow + 1, column];
                bottles[tempRow + 1, column] = null;
                if (bottles[tempRow, column] != null) {
                    // because the empty positions are shifted as well, we need to ensure we don't assign them any positions
                    bottles[tempRow, column].GetComponent<Bottle>().notifyStartDescending(this.computePositionForBottle(tempRow, column));
                    // TODO - check the bug where if both row0 and row1 are empty, row2 becomes weird
                }
            }
        }
    }

    private void addNewRow() {
        int column = 0;
        if (this.totalBottleRows % 2 == 1) {
            column = 1;
        }
        int row = Math.Min(2, this.totalBottleRows);
        for (; column < COLUMN_COUNT; column += 2) {
            bottles[row, column] = Instantiate(bottlePrefabs[this.rnd.Next(2)]);
            bottles[row, column].transform.position = this.computePositionForBottle(row, column);
            Bottle currentBottle = bottles[row, column].GetComponent<Bottle>();
            currentBottle.game = this;
            currentBottle.points = this.rnd.Next(1, 4); // in the interval [1, 3]
        }
        this.totalBottleRows++;
    }

    private void attemptToShiftDownLastRows() {
        while (this.isLastRowDestroyed()) {
            this.shiftRowsDown();
            this.addNewRow();
        }
    }

    public void bottleWasBroken(Bottle brokenBottle) {
        this.currentScorePlayer1 += brokenBottle.points;
        currentScorePlayer1Text.text = string.Format("Score: {0}", this.currentScorePlayer1);
        Invoke("gameOver", 1f);
        attemptToShiftDownLastRows();
    }

    private void gameOver() {
        gameOverScreen.setup(1, 10, 15);
    }
}
