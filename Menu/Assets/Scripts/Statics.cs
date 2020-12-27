using UnityEngine;
using System.Collections.Generic;
public class Statics 
{
    public static bool canSwitchLever = true;
    public static bool sceneWasLeft = false;
    public static bool winPuzzle = false;
    public static bool playPuzzle = false;
    public static int expAfterPuzzle = 0;
    public static Vector3 recentPlayerPosition;
    public static bool isOnRope = false;
    public static string lastSceneId = "";
    public static bool endChest = false;
    public static bool chestOpened = false;
    public static bool itemDropped = false;
    public static bool isImmortal = false;
    public static Dictionary<string, bool> puzzle = new Dictionary<string, bool>()
    {
        { "Puzzle", false },
        { "Puzzle-1-1", false },
        { "Puzzle-1-2", false }
    };
}
