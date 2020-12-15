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

    private Vector3 endPos;
    private bool ifDamaged = false;
    private Vector3 movement;
    private float direction;
    public Animator animator;
    void Start()
    {
        playerStatsScript = player.GetComponent<PlayerUIUpdates>();
        movement = new Vector3(1f, -0.7f, 0f);

    }

    void Update()
    {
        if (ifDamaged)
        {
            float timePassed = 0;
            while (timePassed < 2)
            {
                player.GetComponent<PlayerMovement>().forcePushPlayer();
                timePassed += Time.deltaTime;
            }
            ifDamaged = false;
        }
        else
        {
            ifDamaged = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            direction = (transform.position.x - player.transform.position.x) > 0 ? -1 : 1;
            endPos = player.position + new Vector3(direction * 5f, 1f, 2f);
            ifDamaged = true;
            playerStatsScript.ChangeHealth(damageValue);

            if (playerStatsScript.respawnPlayer())
            {
                if (respawnPoint.transform.name == "RespawnPointMid" || respawnPoint.transform.name == "RespawnPoint2" || respawnPoint.transform.name == "RespawnPointMovingObj")
                {
                    playerStatsScript.respawnPlayerAtCheckpoint();
                    player.transform.position = respawnPoint.transform.position;
                    ifDamaged = false;
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }

        }

    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(2);
        uiObject.SetActive(false);

    }
}

