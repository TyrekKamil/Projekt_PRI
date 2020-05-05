using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
  public Texture2D clickCursor;
  public Texture2D normalCursor;

    void Start() {
        Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    void Update() {
        if(Input.GetMouseButtonDown(0)) {
            Cursor.SetCursor(clickCursor, Vector2.zero, CursorMode.ForceSoftware);
        } else if(Input.GetMouseButtonUp(0)) {
            Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.ForceSoftware);
        }
    }
}
