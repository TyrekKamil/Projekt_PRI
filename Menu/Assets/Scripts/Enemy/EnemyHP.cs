﻿using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Animator animator;

    public bool isBoss;
    public bool endAction = false;
    public float powerAttack = -5f;
    void Start()
    {
        currentHealth = maxHealth;
        GameEvents.SaveInitiated += SaveEnemyData;
        GameEvents.LoadInitiated += LoadEnemyData;
    }

    public void TakeDamage(int damage)
    {   
        endAction = false;
        currentHealth -= damage;
        if (isBoss)
        {
            GetComponent<BossUITakeDamageService>().TakeDamageUI(damage / (maxHealth / 100));
            float timePassed = 0;
            while (timePassed < 1)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(powerAttack * GetComponent<BossController>().direction, 2f), ForceMode2D.Force);
                timePassed += Time.deltaTime;
            }
            Debug.Log(10f * GetComponent<BossController>().direction);

        }
        else
        {
            float timePassed = 0;
            while (timePassed < 1)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(5f * GetComponent<EnemyAnimationController>().direction, 2f), ForceMode2D.Force);
                timePassed += Time.deltaTime;
            }
            endAction = true;
        }
        Debug.Log("Enemy was hit: " + currentHealth + " HP");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameEvents.SaveInitiated -= SaveEnemyData;
        GameEvents.LoadInitiated -= LoadEnemyData;
        Debug.Log("I'm dead");
        animator.Play("Rogue_death_01");
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<RespawnPlayer>().enabled = false;
        if (isBoss)
        {
            GetComponent<BossController>().enabled = false;
        }
        else
        {
            GetComponent<EnemyAnimationController>().enabled = false;

        }

        Destroy(gameObject, 0.5f);
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

        SaveLoad.Save<SerializableEnemy>(serializedEnemy, "Enemy" + this.name);
    }

    private void LoadEnemyData()
    {
        if (SaveLoad.SaveExists("Enemy" + this.name))
        {
            SerializableEnemy enemyData = SaveLoad.Load<SerializableEnemy>("Enemy" + this.name);

            currentHealth = enemyData.health;

            Vector3 position;
            position.x = enemyData.positionX;
            position.y = enemyData.positionY;
            position.z = enemyData.positionZ;

            transform.position = position;
        }

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