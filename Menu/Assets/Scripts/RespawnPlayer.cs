using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RespawnPlayer : MonoBehaviour
{
    public GameObject uiObject;
    public int damageValue;
    Text text;
    PlayerUIUpdates playerStatsScript;
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;


    void Start()
    {
        playerStatsScript = player.GetComponent<PlayerUIUpdates>();

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Other Collider:" + other.name);

        if (other.CompareTag("Player"))
        {

            playerStatsScript.ChangeHealth(damageValue);

            if (playerStatsScript.respawnPlayer())
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

        }

    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(2);
        uiObject.SetActive(false);

    }
}

