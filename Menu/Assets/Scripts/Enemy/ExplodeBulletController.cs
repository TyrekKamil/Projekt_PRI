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
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 0.2f, enemyLayers);
            foreach (Collider2D enemy in enemies)
            {
                enemy.GetComponent<EnemyHP>().TakeDamage(50);
            }
        }
        if (transform.localScale.x > 45)
        {
            Destroy(gameObject);
        }
    }

}
