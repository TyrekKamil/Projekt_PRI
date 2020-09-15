using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] UI UI;
    [SerializeField] Image draggableObject;
    private PipeSlot draggedSlot;
    private PipeSlot[] pipeSlotsArray;
    private void Awake()
    {
        //Setup events:
        //Begin drag
        UI.OnBeginDragEvent += BeginDrag;
        //Stop drag
        UI.OnEndDragEvent += EndDrag;
        //drag
        UI.OnDragEvent += Drag;
        //drop
        UI.OnDropEvent += Drop;
    }
    private void BeginDrag(PipeSlot pipeSlot)
    {
        if (pipeSlot.Pipe != null && pipeSlot.canDrag) {
            draggedSlot = pipeSlot;
            draggableObject.sprite = pipeSlot.Pipe.Icon;
            draggableObject.transform.position = Input.mousePosition;
            draggableObject.enabled = true;
        }
    }
    private void EndDrag(PipeSlot pipeSlot)
    {
        draggedSlot = null;
        draggableObject.enabled = false;
    }
    private void Drag(PipeSlot pipeSlot)
    {
        if (draggableObject.enabled)
        {
            draggableObject.transform.position = Input.mousePosition;
        }
    }

    private void Drop(PipeSlot pipeSlot)
    {
        Pipe draggedPipe = draggedSlot.Pipe;
        draggedSlot.Pipe = pipeSlot.Pipe;
        pipeSlot.Pipe = draggedPipe;
    }





}
