using System;
using System.Collections.Generic;
using UnityEngine;
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

    private void Start()
    {
        for (int i = 0; i < pipeSlots.Length; i++) {
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
    int i = 0;
    string cameFrom;
    public float targetTime = 5.0f;
    bool isDone = false;
    private void Update()
    {
        recentPipe = pipeSlots[i].Pipe;
        targetTime -= Time.deltaTime;
        if (targetTime <= 0.0f && !isDone) {
            try
            {
                if (recentPipe.right && (i + 1) % 8 != 7 && pipeSlots[i + 1].Pipe.left)
                {
                    pipeSlots[i + 1].canDrag = false;
                    recentPipe = pipeSlots[i + 1].Pipe;
                    i += 1;
                    cameFrom = "left";
                    targetTime += 3.0f;
                    Debug.Log("ide w prawo");
                }
                //implementation of edge case
                if (recentPipe.right && i == 38 && pipeSlots[i + 1].Pipe.left)
                {
                    pipeSlots[i + 1].canDrag = false;
                    recentPipe = pipeSlots[i + 1].Pipe;
                    i += 1;
                    cameFrom = "left";
                    targetTime += 3.0f;
                    Debug.Log("ide w prawo");
                }
                if (recentPipe.top && i - 8 > 0 && pipeSlots[i - 8].Pipe.bot && !cameFrom.Equals("top"))
                {
                    pipeSlots[i - 8].canDrag = false;
                    recentPipe = pipeSlots[i - 8].Pipe;
                    i -= 8;
                    cameFrom = "bot";
                    targetTime += 3.0f;
                    Debug.Log("ide do gory");
                }
                if (recentPipe.left && (i - 1) % 8 > 0 && pipeSlots[i - 1].Pipe.right && !cameFrom.Equals("left"))
                {
                    pipeSlots[i - 1].canDrag = false;
                    recentPipe = pipeSlots[i - 1].Pipe;
                    i -= 1;
                    cameFrom = "right";
                    targetTime += 3.0f;
                    Debug.Log("ide w lewo");
                }
                if (recentPipe.bot && i + 8 < pipeSlots.Length && pipeSlots[i + 8].Pipe.top && !cameFrom.Equals("bot"))
                {
                    pipeSlots[i + 8].canDrag = false;
                    recentPipe = pipeSlots[i + 8].Pipe;
                    i += 8;
                    cameFrom = "top";
                    targetTime += 3.0f;
                    Debug.Log("ide w dol.");
                }
            }
            catch (Exception)
            {
                isDone = true;
                Debug.Log("Przegrales");
            }
            if (i == pipeSlots.Length - 1) {
                Debug.Log("Finally");
                isDone = true;
            }
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
            else {
                i--;
            }
        }
    }
    private void AddPipes(Pipe toAdd, int count){
        for (int i = 0; i < count; i++) {
            pipes.Add(toAdd);
        }
    }
}
