using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToLevel : MonoBehaviour
{

    [SerializeField]
    private int sceneId;


    private GameObject textBox;
    private void Awake()
    {
        textBox = transform.Find("Canvas").Find("Description").gameObject;
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
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ActionButton")))) {
            GameObject loader = GameObject.Find("LevelLoader");
            loader.GetComponent<LevelLoader>().LoadLevel(sceneId);

        }
    }
}
