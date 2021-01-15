using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpScript : MonoBehaviour
{
    private GameObject textBox;
    private void Awake()
    {
        textBox = transform.Find("Canvas").Find("Text").gameObject;
        textBox.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        textBox.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        textBox.SetActive(false);
    }
}
