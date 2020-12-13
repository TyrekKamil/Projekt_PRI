using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartChestMinigameTrigger : MonoBehaviour
{
    public GameObject lightBox;
    private bool isZoomed = false;
    public GameObject virtCamera;
    public Transform zoomingObject;
    public InventoryObject inventory;
    private GameObject message;
    private bool removedItem = false;

    void Start()
    {
        if (Statics.endChest)
        {
            lightBox.SetActive(false);
        }
    }

    void Update()
    {
        if (isZoomed)
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize - Time.deltaTime * 2;
        }
        if (isZoomed && Camera.main.orthographicSize < 1.5)
        {
            isZoomed = false;
            SceneManager.LoadScene("Lock");
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {

        if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ActionButton"))) && !Statics.endChest)
        {
            if (inventory.FindItem("Lockpick"))
            {
                if (!removedItem)
                {
                    inventory.ReduceNumberOfItems("Lockpick");
                    removedItem = true;
                }
                
                Statics.recentPlayerPosition = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
                Statics.lastSceneId = SceneManager.GetActiveScene().name;
                Statics.sceneWasLeft = true;
                isZoomed = true;
                virtCamera.SetActive(false);
                Camera.main.transform.position = new Vector3(zoomingObject.position.x, zoomingObject.position.y, Camera.main.transform.position.z);
            }
            else
            {
                Text text;
                message = GameObject.Find("LockpickAreNeededInfo");
                if (!message.activeSelf)
                {
                    message.SetActive(true);
                }
                text = message.GetComponent<Text>();
                text.text = "You need lockpicks to open chest";
                StartCoroutine("WaitForSec");
            }
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(1);
        message.SetActive(false);
    }
}
