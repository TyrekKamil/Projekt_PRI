using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletController : MonoBehaviour
{
    public float speed = 15f;
    public Rigidbody2D rigidbody;
    [SerializeField] private Transform respawnPoint;


    void Start()
    {
        rigidbody.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            Debug.Log("Hit");
            player.GetComponent<PlayerUIUpdates>().ChangeHealth(10);
            if (player.GetComponent<PlayerUIUpdates>().respawnPlayer())
            {
                if (respawnPoint.transform.name == "RespawnPointMid" || respawnPoint.transform.name == "RespawnPoint2" || respawnPoint.transform.name == "RespawnPointMovingObj")
                {
                    player.GetComponent<PlayerUIUpdates>().respawnPlayerAtCheckpoint();
                    player.transform.position = respawnPoint.transform.position;
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }
    }

}
