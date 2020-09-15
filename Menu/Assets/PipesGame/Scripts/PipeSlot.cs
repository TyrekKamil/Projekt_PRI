using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class PipeSlot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] Image Image;
    public bool canDrag = true;
    public event Action<PipeSlot> OnDragEvent;
    public event Action<PipeSlot> OnBeginDragEvent;
    public event Action<PipeSlot> OnEndDragEvent;
    public event Action<PipeSlot> OnDropEvent;
    //colors
    private Color normalColor = Color.white;
    private Color disabledColor = new Color(1, 1, 1, 0);


    private Pipe _pipe;
    public Pipe Pipe {
        get { return _pipe; }
        set {
            _pipe = value;
            if (_pipe == null)
            {
                Image.color = disabledColor;
            }
            else {
                Image.sprite = _pipe.Icon;
                Image.color = normalColor;
            }
        }
    }
    private void OnValidate()
    {
        if (Image == null) {
            Image = GetComponent<Image>();
        }
    }
    Vector2 originalPosition;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (OnBeginDragEvent != null) {
            OnBeginDragEvent(this);
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (OnEndDragEvent != null)
        {
            OnEndDragEvent(this);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragEvent != null)
        {
            OnDragEvent(this);
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (OnDropEvent != null)
        {
            OnDropEvent(this);
        }
    }
}
