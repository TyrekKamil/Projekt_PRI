﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PuzzleCounter : MonoBehaviour
{   
    public int count = 0;
    public void moveCount() {
        count++;
        transform.GetComponent<TextMeshPro>().text = count.ToString();
        transform.GetComponent<MeshRenderer>().sortingLayerName = "Background";
    }
    public void zeroCount() {
        count = 0;
        transform.GetComponent<TextMeshPro>().text = count.ToString();
    }
}
