using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIUpdates : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public PlayerHpExpUI slider;

    void Start()
    {
        slider.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
    }

    public void ChangeHealth(int hit)
    {
        currentHealth -= hit;
        slider.SetHealth(currentHealth);
    }

    public int DisplayHealth()
    {
        return currentHealth;
    }

    public bool respawnPlayer()
    {
        if (currentHealth <= 0)
        {
            return true;
        }
        return false;
    }

    public void respawnPlayerAtCheckpoint()
    {
        slider.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
    }
}
