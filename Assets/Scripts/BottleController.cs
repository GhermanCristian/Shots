using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BottleController : MonoBehaviour {
    private GameObject[,] bottles;
    private int totalBottleRows;

    public GameObject gameController;
    private GameControllerScript game;
    private GameObject[] bottlePrefabs;
    private System.Random rnd; // add the "System" namespace to distinguish from the Unity Random class

    private int position;

    void Start() {
        this.rnd = new System.Random(Guid.NewGuid().GetHashCode());
        this.bottles = new GameObject[Constants.ROW_COUNT, Constants.COLUMN_COUNT];
        this.game = gameController.GetComponent<GameControllerScript>();
        this.bottlePrefabs = game.bottlePrefabs;
        this.totalBottleRows = 0;
    }

    public void setPositionAndGenerateInitialBottles(int position) {
        this.position = position;
        this.generateInitialBottles();
    }

    private void generateInitialBottles() {
        while (this.totalBottleRows < Constants.ROW_COUNT) {
            this.addNewRow();
        }
    }

    private Vector3 computePositionForBottle(int row, int column) {
        return new Vector3(this.position * (column * 0.9f + 1), 2 * row + 0.8f, 0);
    }

    private bool isLastRowDestroyed() {
        for (int column = 0; column < Constants.COLUMN_COUNT; column++) {
            if (bottles[Constants.LAST_ROW_INDEX, column] != null) {
                if (bottles[Constants.LAST_ROW_INDEX, column].GetComponent<Bottle>().isAlive == true) {
                    return false;
                }
            }
        }
        return true;
    }

    private void shiftRowsDown() {
        // "down" in the view, but in the matrix it's actually a move up
        for (int tempRow = 0; tempRow < Constants.ROW_COUNT - 1; tempRow++) {
            for (int column = 0; column < Constants.COLUMN_COUNT; column++) {
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
        int row = Math.Min(Constants.ROW_COUNT - 1, this.totalBottleRows);
        for (; column < Constants.COLUMN_COUNT; column += 2) {
            int points;
            if (this.rnd.Next(10) < 7) {
                bottles[row, column] = Instantiate(bottlePrefabs[Constants.NORMAL_BOTTLE_PREFAB_INDEX]);
                points = 1;
            }
            else {
                bottles[row, column] = Instantiate(bottlePrefabs[Constants.FALLING_BOTTLE_PREFAB_INDEX]);
                points = this.rnd.Next(1, 4) + this.totalBottleRows / 2;
            }
            
            bottles[row, column].transform.position = this.computePositionForBottle(row, column);
            Bottle currentBottle = bottles[row, column].GetComponent<Bottle>();
            currentBottle.game = this.game;
            currentBottle.points = points;
        }
        this.totalBottleRows++;
    }

    public void attemptToShiftDownLastRows() {
        while (this.isLastRowDestroyed()) {
            this.shiftRowsDown();
            this.addNewRow();
        }
    }
}
