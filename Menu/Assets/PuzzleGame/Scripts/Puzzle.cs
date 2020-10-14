using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour {
    public int blocksPerLine = 3;

    void Start() {
        CreatePuzzle();
    }
    void CreatePuzzle() { 
        for (int x = 0; x < blocksPerLine; x++) {
            for (int y = 0; y < blocksPerLine; y++) {
                GameObject blockObj = GameObject.CreatePrimitive(PrimitiveType.Quad);
                blockObj.transform.position = -Vector2.one * (blocksPerLine - 1) * 0.5f + new Vector2(x,y);
                blockObj.transform.parent = transform;
            }
        }
        Camera.main.orthographicSize = blocksPerLine * 0.55f;
    }

}
