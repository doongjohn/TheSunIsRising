using UnityEngine;

public static class KeySettings
{
    private static KeyCode moveRight = KeyCode.RightArrow;
    private static KeyCode moveLeft = KeyCode.LeftArrow;
    private static KeyCode jump = KeyCode.UpArrow;
    private static KeyCode groundSlam = KeyCode.DownArrow;
    private static KeyCode moveIsland = KeyCode.Space;

    public static KeyCode MoveRight => moveRight;
    public static KeyCode MoveLeft => moveLeft;
    public static KeyCode Jump => jump;
    public static KeyCode GroundSlam => groundSlam;
    public static KeyCode MoveIsland => moveIsland;
}
