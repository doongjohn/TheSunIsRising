using UnityEngine;

public static class KeySettings
{
    private static KeyCode moveRight = KeyCode.RightArrow;
    private static KeyCode moveLeft = KeyCode.LeftArrow;
    private static KeyCode jump = KeyCode.Space;
    private static KeyCode groundSlam = KeyCode.DownArrow;

    public static KeyCode MoveRight => moveRight;
    public static KeyCode MoveLeft => moveLeft;
    public static KeyCode Jump => jump;
    public static KeyCode GroundSlam => groundSlam;
}
