using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RespawnPlayer : MonoBehaviour
{
    public GameObject uiObject;
    Text text;
    public int deathRate = 3;
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            deathRate -= 1;

            uiObject.SetActive(true);
            text = uiObject.GetComponent<Text>();
            text.text = deathRate + " lives left!";
            StartCoroutine("WaitForSec");

            if (deathRate == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            player.transform.position = respawnPoint.transform.position;
        }

    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(1);
        uiObject.SetActive(false);

    }
}

