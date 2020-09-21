using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] int horizontalPipes;
    [SerializeField] Pipe horizontalPipe;

    [SerializeField] int verticalPipes;
    [SerializeField] Pipe verticalPipe;

    [SerializeField] int botRightPipes;
    [SerializeField] Pipe botRightPipe;

    [SerializeField] int leftBotPipes;
    [SerializeField] Pipe leftBotPipe;

    [SerializeField] int leftTopPipes;
    [SerializeField] Pipe leftTopPipe;

    [SerializeField] int topRightPipes;
    [SerializeField] Pipe topRightPipe;

    private List<Pipe> pipes = new List<Pipe>();
    [SerializeField] Transform pipesParent;
    [SerializeField] PipeSlot[] pipeSlots;

    public event Action<PipeSlot> OnDragEvent;
    public event Action<PipeSlot> OnBeginDragEvent;
    public event Action<PipeSlot> OnEndDragEvent;
    public event Action<PipeSlot> OnDropEvent;

    public GameObject tryAgainText;

    private void Start()
    {
        for (int i = 0; i < pipeSlots.Length; i++)
        {
            pipeSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            pipeSlots[i].OnDragEvent += OnDragEvent;
            pipeSlots[i].OnDropEvent += OnDropEvent;
            pipeSlots[i].OnEndDragEvent += OnEndDragEvent;
        }
        GenerateStartingPattern();
    }
    private void OnValidate()
    {
        if (pipesParent != null)
        {
            pipeSlots = pipesParent.GetComponentsInChildren<PipeSlot>();
        }
        GenerateStartingPattern();
    }
    Pipe recentPipe;
    PipeSlot recentSlot;
    int i = 0;
    string cameFrom = "null";
    public float targetTime = 5.0f;
    bool isDone = false;
    public float animationTimer;
    int currentTile = 0;
    bool fail = false;
    public float waitingTime = 999.0f;
    private void Update()
    {
        waitingTime -= Time.deltaTime;
        if (isDone && waitingTime <= -3.0f)
        {
            if (fail)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                SceneManager.LoadScene("Level_1");
            }
        }
        recentSlot = pipeSlots[i];
        recentSlot.canDrag = false;
        recentPipe = pipeSlots[i].Pipe;
        animationTimer += Time.deltaTime;
        AnimateTile(recentSlot, recentPipe);
        targetTime -= Time.deltaTime;
        if (targetTime <= 0.0f && !isDone)
        {
            currentTile = 0;
            animationTimer = 0.0f;
            try
            {
                //Check if there's connection to the right
                if ((recentPipe.right && (i + 1) % 8 != 7 && pipeSlots[i + 1].Pipe.left && !cameFrom.Equals("right")))
                {
                    i += 1;
                    cameFrom = "left";
                    Debug.Log("ide w prawo");
                }
                //Check if there's connection to the top
                else if (recentPipe.top && i - 8 > 0 && pipeSlots[i - 8].Pipe.bot && !cameFrom.Equals("top"))
                {
                    i -= 8;
                    cameFrom = "bot";
                    Debug.Log("ide do gory");
                }
                //Check if there's connection to the left
                else if (recentPipe.left && (i - 1) % 8 >= 0 && pipeSlots[i - 1].Pipe.right && !cameFrom.Equals("left"))
                {
                    i -= 1;
                    cameFrom = "right";
                    Debug.Log("ide w lewo");
                }
                //Check if there's connection to the down
                else if (recentPipe.bot && i + 8 < pipeSlots.Length && pipeSlots[i + 8].Pipe.top && !cameFrom.Equals("bot"))
                {
                    i += 8;
                    cameFrom = "top";
                    Debug.Log("ide w dol.");
                }
                //implementation of edge case
                else if (recentPipe.right && i == 38 && pipeSlots[i + 1].Pipe.left)
                {
                    recentPipe = pipeSlots[i + 1].Pipe;
                    i += 1;
                    Debug.Log("ide w prawo");
                }
                else {
                    isDone = true;
                    waitingTime = 0.0f;
                    fail = true;
                    tryAgainText.SetActive(true);
                    Debug.Log("Przegrales");
                }
                targetTime += 3.0f;
            }
            catch (Exception)
            {
                isDone = true;
                waitingTime = 0.0f;
                fail = true;
                tryAgainText.SetActive(true);
                Debug.Log("Przegrales");

            }
            if (i == pipeSlots.Length - 1)
            {
                waitingTime = 0.0f;
                Debug.Log("Finally");
                isDone = true;
            }
        }

    }

    private void AnimateTile(PipeSlot pipeSlot, Pipe pipe)
    {
        if (targetTime <= -500.0f) {
            pipeSlot.Image.sprite = pipe.frameArrayDefault[pipe.frameArrayDefault.Length - 1];
        }
        else if (animationTimer >= targetTime / pipe.frameArrayDefault.Length && currentTile < pipe.frameArrayDefault.Length)
        {
            animationTimer -= targetTime / pipe.frameArrayDefault.Length;
            //right & bot;
            if (pipe.bot && pipe.right)
            {
                if (cameFrom.Equals("bot"))
                {
                    pipeSlot.Image.sprite = pipe.frameArrayDefault[currentTile];
                }
                else
                {
                    pipeSlot.Image.sprite = pipe.frameArrayInverted[currentTile];
                }
            }

            //right & top
            else if (pipe.right && pipe.top)
            {
                if (cameFrom.Equals("top"))
                {
                    pipeSlot.Image.sprite = pipe.frameArrayDefault[currentTile];
                }
                else
                {
                    pipeSlot.Image.sprite = pipe.frameArrayInverted[currentTile];
                }
            }

            //left & bot
            else if (pipe.left && pipe.bot)
            {
                if (cameFrom.Equals("left"))
                {
                    pipeSlot.Image.sprite = pipe.frameArrayDefault[currentTile];
                }
                else
                {
                    pipeSlot.Image.sprite = pipe.frameArrayInverted[currentTile];
                }
            }

            //left & top
            else if (pipe.left && pipe.top)
            {
                if (cameFrom.Equals("top"))
                {
                    pipeSlot.Image.sprite = pipe.frameArrayDefault[currentTile];
                }
                else
                {
                    pipeSlot.Image.sprite = pipe.frameArrayInverted[currentTile];
                }
            }

            //top & bot, left & right
            else if (((pipe.left && pipe.right) || (pipe.top && pipe.bot)))
            {
                if (cameFrom.Equals("left") || cameFrom.Equals("top") || cameFrom.Equals("null"))
                {
                    pipeSlot.Image.sprite = pipe.frameArrayDefault[currentTile];
                }
                else
                {
                    pipeSlot.Image.sprite = pipe.frameArrayInverted[currentTile];
                }
            }

            currentTile++;
        }
    }
    private void GenerateStartingPattern()
    {
        pipes.Clear();
        for (int i = 0; i < pipeSlots.Length; i++)
        {
            pipeSlots[i].Pipe = null;
        }
        pipeSlots[0].Pipe = horizontalPipe;
        pipeSlots[0].canDrag = false;
        pipeSlots[pipeSlots.Length - 1].Pipe = horizontalPipe;
        pipeSlots[pipeSlots.Length - 1].canDrag = false;
        System.Random random = new System.Random();
        //Adding all pipes to the list sd
        AddPipes(horizontalPipe, horizontalPipes);
        AddPipes(verticalPipe, verticalPipes);
        AddPipes(botRightPipe, botRightPipes);
        AddPipes(leftBotPipe, leftBotPipes);
        AddPipes(leftTopPipe, leftTopPipes);
        AddPipes(topRightPipe, topRightPipes);
        int x = 0;
        int numberOfSlots = pipeSlots.Length;
        for (int i = 0; i < pipes.Count; i++)
        {
            x = random.Next(1, numberOfSlots - 1);
            if (pipeSlots[x].Pipe == null)
            {
                pipeSlots[x].Pipe = pipes[i];
            }
            else
            {
                i--;
            }
        }
    }
    private void AddPipes(Pipe toAdd, int count)
    {
        for (int i = 0; i < count; i++)
        {
            pipes.Add(toAdd);
        }
    }
}
