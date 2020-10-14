using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBlock : MonoBehaviour {
    public event System.Action<PuzzleBlock> OnBlockPressed;
    public Vector2Int coord;

    public void Init(Vector2Int startingCoord, Texture2D image) {
        coord = startingCoord;
        GetComponent<MeshRenderer>().material.mainTexture = image;
    }
    private void OnMouseDown () {
        if (OnBlockPressed != null) {
            OnBlockPressed (this);
        }
    }
}