using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RespawnPlayer : MonoBehaviour
{
    public GameObject uiObject;
    Text text;
    PlayerUIUpdates triggerScript;
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;


    void Start()
    {
        triggerScript = player.GetComponent<PlayerUIUpdates>();

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Other Collider:" + other.name);

        if (other.CompareTag("Player"))
        {

            triggerScript.ChangeHealth(1);

            if (triggerScript.DisplayHealth() == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                player.transform.position = respawnPoint.transform.position;

                uiObject.SetActive(true);
                text = uiObject.GetComponent<Text>();
                text.text = triggerScript.DisplayHealth() + " lives left!";
                StartCoroutine("WaitForSec");
            }

        }

    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(2);
        uiObject.SetActive(false);

    }
}

