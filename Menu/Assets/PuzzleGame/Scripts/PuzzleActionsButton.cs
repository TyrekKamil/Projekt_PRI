using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleActionsButton : MonoBehaviour {
    public GameObject puzzleGameObject;
    public void restartPuzzleButton() {
        puzzleGameObject.GetComponent<Puzzle>().restartPuzzle();
        // counter to zero
    }
       public void exitPuzzleButton() {
        puzzleGameObject.GetComponent<Puzzle>().exitPuzzle();
    }

}
