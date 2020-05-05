using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
   // private SpriteRenderer rend;
  // public Sprite clickCursor;
  //  public Sprite normalCursor;
  public Texture2D clickCursor;
  public Texture2D normalCursor;

    void Start() {
        //Cursor.visible = false;\
        Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    void Update() {
               if(Input.GetMouseButtonDown(0)) {
        Cursor.SetCursor(clickCursor, Vector2.zero, CursorMode.ForceSoftware);
        } else if(Input.GetMouseButtonUp(0)) {
        Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.ForceSoftware);
        }
 /*       Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;
		
        if(Input.GetMouseButtonDown(0)) {
            rend.sprite = clickCursor;
        } else if(Input.GetMouseButtonUp(0)) {
            rend.sprite = normalCursor;
        }*/
    }
}
