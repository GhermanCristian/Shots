using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameControllerScript : MonoBehaviour {
    public GameObject bottlePrefab;
    private GameObject[,] bottles = new GameObject[3, 11];
    private int totalBottleRows;

    private void generateBottles() {
        while (this.totalBottleRows < 3) {
            this.addNewRow();
        }
    }

    void Start() {
        this.totalBottleRows = 0;
        generateBottles();
    }

    private bool isLastRowDestroyed() {
        for (int column = 0; column < 11; column++) {
            if (bottles[0, column] != null) {
                return false;
            }
        }
        return true;
    }

    private void shiftRowsDown() {
        // "down" in the view, but in the matrix it's actually a move up
        for (int tempRow = 0; tempRow < 2; tempRow++) {
            for (int column = 0; column < 11; column++) {
                bottles[tempRow, column] = bottles[tempRow + 1, column];
                bottles[tempRow + 1, column] = null;
                if (bottles[tempRow, column] != null) {
                    // because the empty positions are shifted as well, we need to ensure we don't assign them any positions
                    bottles[tempRow, column].transform.position = new Vector3(-column - 0.5f, 2 * tempRow + 1, 0);
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
        for (; column < 11; column += 2) {
            bottles[row, column] = Instantiate(bottlePrefab);
            bottles[row, column].transform.position = new Vector3(-column - 0.5f, 2 * row + 1, 0);
        }
        this.totalBottleRows++;
    }

    private void attemptToShiftRowsDown() {
        while (this.isLastRowDestroyed()) {
            this.shiftRowsDown();
            this.addNewRow();
        }
    }

    public void fireShot(PlayerScript player) {
        Invoke("attemptToShiftRowsDown", 0.5f);
    }
}
