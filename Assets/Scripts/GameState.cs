using System;
public class GameDataCenter
{
    public static GameState curState = GameState.NONE;
}

public enum GameState
{
    NONE,
    PLAY,
    MAGIC_PLAY,
    STOP,
    REPLAY,
    RESET
}