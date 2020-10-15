using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Puzzle : MonoBehaviour {
    public int blocksPerLine = 3;
    private PuzzleBlock emptyBlock;
    private Queue<PuzzleBlock> blocksQueue;
    private bool blockIsMoving;
    public Texture2D image;
    public float duration = 0.2f;
    void Start () {
        CreatePuzzle ();
    }
    private void CreatePuzzle () {
        Texture2D[, ] images = ImageSlicer.GetSlices (image, blocksPerLine);
        for (int x = 0; x < blocksPerLine; x++) {
            for (int y = 0; y < blocksPerLine; y++) {
                GameObject blockObj = GameObject.CreatePrimitive (PrimitiveType.Quad);
                blockObj.transform.position = -Vector2.one * (blocksPerLine - 1) * 0.5f + new Vector2 (x, y);
                blockObj.transform.parent = transform;

                PuzzleBlock puzzleBlock = blockObj.AddComponent<PuzzleBlock> ();
                puzzleBlock.OnBlockPressed += AddMoveBlockToQueue;
                puzzleBlock.OnFinishedMoving += OnFinishedMoving;
                puzzleBlock.Init (new Vector2Int (x, y), images[x, y]);

                if (y == 0 && x == blocksPerLine - 1) {
                    blockObj.SetActive (false);
                    emptyBlock = puzzleBlock;
                }
            }
        }
        Camera.main.orthographicSize = blocksPerLine * 0.65f;
        blocksQueue = new Queue<PuzzleBlock>();
    }

    private void AddMoveBlockToQueue (PuzzleBlock puzzleToMove) {
        blocksQueue.Enqueue (puzzleToMove);
        MakeNextPlayerMove();
    }

    private void MakeNextPlayerMove () {
        while (blocksQueue.Count > 0 && !blockIsMoving) {
            PlayerMoveBlockInput (blocksQueue.Dequeue ());
        }
    }
    private void PlayerMoveBlockInput (PuzzleBlock puzzleToMove) {
        if (Mathf.Floor ((puzzleToMove.transform.position - emptyBlock.transform.position).sqrMagnitude) == 1) {
            Vector2Int targetCoord = emptyBlock.coord;
            emptyBlock.coord = puzzleToMove.coord;
            puzzleToMove.coord = targetCoord;

            Vector2 targetPosition = emptyBlock.transform.position;
            emptyBlock.transform.position = puzzleToMove.transform.position;
            puzzleToMove.MoveToPosition (targetPosition, duration);
        }
    }

    private void OnFinishedMoving () {
        blockIsMoving = false;
        MakeNextPlayerMove();
    }

}