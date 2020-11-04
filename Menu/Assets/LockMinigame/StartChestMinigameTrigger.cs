using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartChestMinigameTrigger : MonoBehaviour
{
    public GameObject lightBox;
    void Start()
    {
        if (Statics.endChest)
        {
            lightBox.SetActive(false);
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {

        if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ActionButton"))) && !Statics.endChest)
        {
            Statics.recentPlayerPosition = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
            Statics.lastSceneId = SceneManager.GetActiveScene().name;
            Statics.sceneWasLeft = true;
            SceneManager.LoadScene("Lock");
        }
    }
}
