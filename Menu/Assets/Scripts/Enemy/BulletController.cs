using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletController : MonoBehaviour
{
    public float speed = 15f;
    public Rigidbody2D rigidbody;
    [SerializeField] private Transform respawnPoint;
    public bool enemyBullet = true;
    public LayerMask enemyLayers;



    void Start()
    {
        if (!enemyBullet)
        {
            GameObject player = GameObject.Find("Player");
            float direction = player.transform.localScale.x * 2;
            rigidbody.velocity = transform.right * speed * direction;
        }
        else
        {
            rigidbody.velocity = transform.right * speed;
        }
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player" && enemyBullet)
        {
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
            Destroy(this);
        }
    }

    void Update()
    {
        if (!enemyBullet)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 0.2f, enemyLayers);
            foreach (Collider2D enemy in enemies)
            {
                enemy.GetComponent<EnemyHP>().TakeDamage(100);
                Destroy(this);
            }
        }
    }

}
