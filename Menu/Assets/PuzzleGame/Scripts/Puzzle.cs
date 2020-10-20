using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Puzzle : MonoBehaviour
{
    public int blocksPerLine = 3;
    private PuzzleBlock emptyBlock;
    private Queue<PuzzleBlock> blocksQueue;
    private bool blockIsMoving;
    public Texture2D image;
    private bool[] isOnBoard;
    public float duration = 0.2f;
    private PuzzleBlock[] puzzleBlocks;

    public GameObject counterPuzzle;
    void Start()
    {
        CreatePuzzle();
    }
    private void CreatePuzzle()
    {
        generateBoolArray();
        puzzleBlocks = new PuzzleBlock[blocksPerLine * blocksPerLine];
        Texture2D[,] images = ImageSlicer.GetSlices(image, blocksPerLine);
        for (int x = 0; x < blocksPerLine; x++)
        {
            for (int y = 0; y < blocksPerLine; y++)
            {
                GameObject blockObj = GameObject.CreatePrimitive(PrimitiveType.Quad);
                int pos = randomPosition();
                int blockObj_x = generateX(pos);
                int blockObj_y = generateY(pos);

                blockObj.transform.position = -Vector2.one * (blocksPerLine - 1) * 0.5f + new Vector2(blockObj_x, blockObj_y);
                blockObj.transform.parent = transform;

                PuzzleBlock puzzleBlock = blockObj.AddComponent<PuzzleBlock>();
                puzzleBlock.OnBlockPressed += AddMoveBlockToQueue;
                puzzleBlock.OnFinishedMoving += OnFinishedMoving;
                int order = (blocksPerLine * blocksPerLine) - (blocksPerLine - (x + 1) + y * blocksPerLine);
                puzzleBlock.Init(new Vector2Int(blockObj_x, blockObj_y), images[x, y], order);
                puzzleBlocks[blocksPerLine * x + y] = puzzleBlock;
                if (y == 0 && x == blocksPerLine - 1)
                {
                    blockObj.SetActive(false);
                    emptyBlock = puzzleBlock;
                }
            }
        }
        Camera.main.orthographicSize = blocksPerLine * 0.65f;
        blocksQueue = new Queue<PuzzleBlock>();
    }

    private void AddMoveBlockToQueue(PuzzleBlock puzzleToMove)
    {
        blocksQueue.Enqueue(puzzleToMove);
        MakeNextPlayerMove();
    }

    private void MakeNextPlayerMove()
    {
        while (blocksQueue.Count > 0 && !blockIsMoving)
        {
            PlayerMoveBlockInput(blocksQueue.Dequeue());
        }
    }
    private void PlayerMoveBlockInput(PuzzleBlock puzzleToMove)
    {
        if (Mathf.Floor((puzzleToMove.transform.position - emptyBlock.transform.position).sqrMagnitude) == 1)
        {
            Vector2Int targetCoord = emptyBlock.coord;
            emptyBlock.coord = puzzleToMove.coord;
            puzzleToMove.coord = targetCoord;

            Vector2 targetPosition = emptyBlock.transform.position;
            emptyBlock.transform.position = puzzleToMove.transform.position;
            puzzleToMove.MoveToPosition(targetPosition, duration);
        }
    }

    private void OnFinishedMoving()
    {
        blockIsMoving = false;
        counterPuzzle.GetComponent<PuzzleCounter>().moveCount();
        MakeNextPlayerMove();
        CheckPuzzles();
    }

    private void CheckPuzzles()
    {
        bool success = true;
        for (int i = 0; i < blocksPerLine * blocksPerLine; i++)
        {
            if (i != (puzzleBlocks[i].coord.y + puzzleBlocks[i].coord.x * blocksPerLine))
            {
                success = false;
            }
        }
        Debug.Log(success);
    }
    private int randomPosition()
    {
        int pos = 0;
        do
        {
            pos = new System.Random().Next(0, (blocksPerLine * blocksPerLine));
        }
        while (isOnBoard[pos]);
        isOnBoard[pos] = true;
        return pos;
    }
    private int generateX(int pos)
    {
        return (pos % blocksPerLine);
    }
    private int generateY(int pos)
    {
        return (pos / blocksPerLine);
    }

    private void generateBoolArray()
    {
        isOnBoard = new bool[blocksPerLine * blocksPerLine];
        for (int x = 0; x < blocksPerLine * blocksPerLine; x++)
        {
            isOnBoard[x] = false;
        }
    }
    public void restartPuzzle() {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
        CreatePuzzle();
    }

}