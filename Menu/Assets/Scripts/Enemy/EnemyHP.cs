using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public string id;

    public Animator animator;
    public EnemyData enemyData;

    void Start() {
        currentHealth = maxHealth;

        if (string.IsNullOrEmpty(enemyData.id))
        {
            enemyData.id = id;
            SaveData.current.enemyData.Add(enemyData);
        }

        GameEvents.current.onLoadEvent += DestroyEnemy;
    }

    private void Update()
    {
        enemyData.position = transform.position;
        enemyData.rotation = transform.rotation;
    }


    public void TakeDamage(int damage) {
        currentHealth -= damage;
        Debug.Log("Enemy was hit: " + currentHealth + " HP");
        if(currentHealth <= 0) {
            Die();
        }
    }

    void Die() {
        SaveData.current.enemyData.Remove(enemyData);

        Debug.Log("I'm dead");
        animator.Play("Rogue_death_01");
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<RespawnPlayer>().enabled = false;
        GetComponent<EnemyAnimationController>().enabled = false;

        Destroy(gameObject);
    }

    private void DestroyEnemy()
    {
        GameEvents.current.onLoadEvent -= DestroyEnemy;
        Destroy(gameObject);
    }


}
