using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    private SpriteRenderer rend;
    public Sprite clickCursor;
    public Sprite normalCursor;

    void Start() {
        Cursor.visible = false;
        rend = GetComponent<SpriteRenderer>();
    }

    void Update() {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;

        if(Input.GetMouseButtonDown(0)) {
            rend.sprite = clickCursor;
        } else if(Input.GetMouseButtonUp(0)) {
            rend.sprite = normalCursor;
        }
    }
}
