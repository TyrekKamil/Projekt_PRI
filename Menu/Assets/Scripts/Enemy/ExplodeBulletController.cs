using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExplodeBulletController : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    private float scaleSpeed = 0.2f;
    public LayerMask enemyLayers;

    void Update()
    {
        if (gameObject.active)
        {
            transform.localScale = transform.localScale + new Vector3(scaleSpeed, scaleSpeed, scaleSpeed);
        }
        if (transform.localScale.x > 45)
        {
            gameObject.SetActive(false);
            transform.localScale = new Vector3(4, 4, 4);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyHP>().TakeDamage(100);
        }
    }

}
