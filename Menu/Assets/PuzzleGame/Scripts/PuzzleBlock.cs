using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBlock : MonoBehaviour {
    public event System.Action<PuzzleBlock> OnBlockPressed;
    public event System.Action OnFinishedMoving;
    public Vector2Int coord;

    public void Init (Vector2Int startingCoord, Texture2D image) {
        coord = startingCoord;
        GetComponent<MeshRenderer> ().material.mainTexture = image;
    }

    public void MoveToPosition (Vector2 target, float duration) {
        StartCoroutine (SmoothMove (target, duration));
    }
    private void OnMouseDown () {
        if (OnBlockPressed != null) {
            OnBlockPressed (this);
        }
    }

    IEnumerator SmoothMove (Vector2 target, float duration) {
        Vector2 initPos = transform.position;
        float percent = 0;
        while (percent < 1) {
            percent += Time.deltaTime / duration;
            transform.position = Vector2.Lerp (initPos, target, percent);
            yield return null;
        }
        if (OnFinishedMoving != null) {
            OnFinishedMoving ();
        }
    }
}