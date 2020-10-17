using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public Animator animator;
    void Start() {
        currentHealth = maxHealth;
    }


    public void TakeDamage(int damage) {
        currentHealth -= damage;
        Debug.Log("Enemy was hit: " + currentHealth + " HP");
        if(currentHealth <= 0) {
            Die();
        }
    }

    void Die() {
        Debug.Log("I'm dead");
        animator.Play("Rogue_death_01");
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<RespawnPlayer>().enabled = false;
        GetComponent<EnemyAnimationController>().enabled = false;
        StartCoroutine(DestroyObject());
    }
    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);

    }
}
