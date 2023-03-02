
using UnityEngine;

public static class TagAndKey
{
    public const string
        T_PLAYER = "Player",
        T_GROUND = "Ground",
        T_DEATHZONE = "DeathZone",
        T_ENEMY = "Enemy",
        T_DEATHPOINTENEMY = "DeathPointEnemy",
        T_WALL = "Wall",
        T_ITEM = "Item",
        T_MAINCAM = "MainCamera",
        T_FINISH = "Finish",
        A_PLAYER_HURT = "Hurt",
        A_PLAYER_JUMPUP = "JumpUp",
        A_PLAYER_FALL = "Fall",
        A_PLAYER_CROUND = "Cround",
        A_PLAYER_IDLE = "Idle",
        A_PLAYER_RUN = "Run";

    public const int STATE_BLACKHOLE = 3,STATE_CHERRY = 1, STATE_GEM = 2,STATE_SCORE = 4,
        STATE_DIALOGPAUSE = 3,STATE_DIALOGWIN = 1,STATE_DIALOGOVER = 2,POINT_CHERRY = 10,POINT_GEM = 35;
    
}
