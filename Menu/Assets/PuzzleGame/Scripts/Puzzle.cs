using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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
    public GameObject puzzleMidground;
    private bool onExit = false;
    private bool success;
    public GameObject winText;
    public PuzzleActionsButton pab;
    void Start()
    {
        CreatePuzzle();
    }
    private void CreatePuzzle()
    {
        generateBoolArray();
        puzzleBlocks = new PuzzleBlock[blocksPerLine * blocksPerLine];
        Texture2D[,] images = ImageSlicer.GetSlices(image, blocksPerLine);
        int[] numbersInOrder = new int[blocksPerLine * blocksPerLine];
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
                if (order == 9)
                    numbersInOrder[pos] = -1;
                else
                numbersInOrder[pos] = order;
                puzzleBlock.Init(new Vector2Int(blockObj_x, blockObj_y), images[x, y], order);
                puzzleBlocks[blocksPerLine * x + y] = puzzleBlock;
                if (y == 0 && x == blocksPerLine - 1)
                {
                    blockObj.SetActive(false);
                    emptyBlock = puzzleBlock;
                }
            }
        }
        int[] numbersOrdered = new int[blocksPerLine * blocksPerLine];
        int j = 0;
        for (int i = 6; i < 9; i++) {
            if (numbersInOrder[i] != -1)
            {
                numbersOrdered[j] = numbersInOrder[i];
                j++;
            }
        }
        for (int i = 3; i < 6; i++)
        {
            if (numbersInOrder[i] != -1)
            {
                numbersOrdered[j] = numbersInOrder[i];
                j++;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (numbersInOrder[i] != -1)
            {
                numbersOrdered[j] = numbersInOrder[i];
                j++;
            }
        }
        int inversions = 0;
        for (int i = 0; i < numbersOrdered.Length - 1; i++) {
            for (int j1 = i + 1; j1 < numbersOrdered.Length - 1; j1++) {
                if (numbersOrdered[j1] > numbersOrdered[i]) {
                    inversions++;
                }
            }
            
        }
        if (inversions % 2 == 1)
        {
            pab.restartPuzzleButton();
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
        success = true;
        for (int i = 0; i < blocksPerLine * blocksPerLine; i++)
        {
            if (i != (puzzleBlocks[i].coord.y + puzzleBlocks[i].coord.x * blocksPerLine))
            {
                success = false;
            }
        }
        if (success) {
            exitPuzzle();
        }
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
        counterPuzzle.GetComponent<PuzzleCounter>().zeroCount();
        CreatePuzzle();
    }

    public void exitPuzzle() { 
        if (success) {
            GLOBAL_DATA.Instance.winPuzzle = true;
            Statics.puzzle[SceneManager.GetActiveScene().name] = true;
            int exp = 100 - (counterPuzzle.GetComponent<PuzzleCounter>().count / 25) * 10;
            GLOBAL_DATA.Instance.expAfterPuzzle = exp <= 0 ? 5 : exp;
            winText.GetComponent<TextMeshPro>().enabled = true;
            winText.GetComponent<TextMeshPro>().text = winText.GetComponent<TextMeshPro>().text.Replace("pointexp", GLOBAL_DATA.Instance.expAfterPuzzle.ToString() + "%");
        }
        Statics.playPuzzle = true;
        puzzleMidground.GetComponent<PuzzleBackgroundOnStart>().buttons.SetActive(false);
        onExit = true;
    }

    void Update() {
        if(onExit && puzzleMidground.transform.position.x >= 0 ) {
            puzzleMidground.transform.position += (new Vector3(-2f, 0, 0) * Time.deltaTime);
        } else if (onExit) {
            SceneManager.LoadScene(Statics.lastSceneId);        
        }
    }
}