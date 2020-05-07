using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
  public Texture2D clickCursor;
  public Texture2D normalCursor;
  public Vector2 hotSpot = new Vector2(25, 15);


    void Start() {
        Cursor.SetCursor(normalCursor, hotSpot, CursorMode.ForceSoftware);
    }

    void Update() {
        if(Input.GetMouseButtonDown(0)) {
            Cursor.SetCursor(clickCursor, hotSpot, CursorMode.ForceSoftware);
        } else if(Input.GetMouseButtonUp(0)) {
            Cursor.SetCursor(normalCursor, hotSpot, CursorMode.ForceSoftware);
        }
    }
}
