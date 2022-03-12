using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants {
    public const int PLAYER_1_INDEX = 0;
    public const int PLAYER_2_INDEX = 1;

    public const int PLAYER_1_NUMBER = 1;
    public const int PLAYER_2_NUMBER = 2;

    public const int PLAYER_1_POSITION = -1;
    public const int PLAYER_2_POSITION = 1;

    public const int BOMB_COST = 12;

    public const string DEFAULT_SCORE_TEXT = "Score: 0";

    public const float HIGHEST_Y_COORD = 6.1f;
    public const float LOWEST_Y_COORD = -6.1f;

    public const float BULLET_SPEED = 0.05f;
    public const float BOMB_SPEED = 0.03f;
    public const float FALLING_BOTTLE_SPEED_MULTIPLIER = 0.01f;

    public const string DESTROYED_BOTTLE_ANIMATION_KEY = "isAlive";

    public const int ROW_COUNT = 3;
    public const int COLUMN_COUNT = 11;
    public const int LAST_ROW_INDEX = 0; // the lowest row in the game view
    public const int NORMAL_BOTTLE_PREFAB_INDEX = 0;
    public const int FALLING_BOTTLE_PREFAB_INDEX = 1;

    public const string MAIN_MENU_SCENE_NAME = "MainMenuScene";
    public const string GAME_SCENE_NAME = "GameScene";

    public const float PLAYER_HORIZONTAL_SPEED = 7f;
    public const float PLAYER_SIZE = 1; // square
    public const float HIGHEST_Y_COORD_PLAYER = HIGHEST_Y_COORD - PLAYER_SIZE / 2;
    public const float LOWEST_Y_COORD_PLAYER = LOWEST_Y_COORD + PLAYER_SIZE / 2;
    public const float PLAYER_JUMPING_IMPULSE = 280f;
}
