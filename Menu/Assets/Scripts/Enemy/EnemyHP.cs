using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public Animator animator;

    private EnemyHandler enemyHandler = new EnemyHandler();
    private CollectibleEnemySet collectibleEnemySet;
    private EnemyID enemyID;


    void Start() {
        currentHealth = maxHealth;
        GameEvents.SaveInitiated += SaveEnemyData;
        collectibleEnemySet = FindObjectOfType<CollectibleEnemySet>();
        enemyID = GetComponent<EnemyID>();

        if (collectibleEnemySet.killedEnemies.Contains(enemyID.ID))
        {
            Debug.Log(enemyID.ID);
        }
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        Debug.Log("Enemy was hit: " + currentHealth + " HP");
        if(currentHealth <= 0) {
            Die();
        }
    }

    void Die() {
        collectibleEnemySet.killedEnemies.Add(enemyID.ID);

        Debug.Log("I'm dead");
        animator.Play("Rogue_death_01");
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<RespawnPlayer>().enabled = false;
        GetComponent<EnemyAnimationController>().enabled = false;

        Destroy(gameObject,0.5f);
    }

    private void SaveEnemyData()
    {
        SerializableEnemy serializedEnemy = new SerializableEnemy()
        {
            enemyName = this.name,
            health = this.currentHealth,
            positionX = transform.position.x,
            positionY = transform.position.y,
            positionZ = transform.position.z,
        };
    }


}

[System.Serializable]
public class SerializableEnemy
{
    public string enemyName { get; set; }
    public int health { get; set; }
    public float positionX { get; set; }
    public float positionY { get; set; }
    public float positionZ { get; set; }

}