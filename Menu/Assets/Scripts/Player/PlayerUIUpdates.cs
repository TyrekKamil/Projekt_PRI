using System;
using TMPro;
using UnityEngine;

public class PlayerUIUpdates : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public TextMeshProUGUI currentLevel, currentLevelPercentage;
    public ParticleSystem onLevelUpEffect;

    public PlayerHpExpUI slider;
    public PlayerLevelingSystem playerLevelingSystem;



    void Start()
    {
        playerLevelingSystem = new PlayerLevelingSystem(1, OnLevelUp);
        slider.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
    }

    public void OnLevelUp()
    {
        print("New level! Now you are level: " + playerLevelingSystem.currentLevel);
        int oldEXP = playerLevelingSystem.experience;
        int newexp = playerLevelingSystem.GetXPforLevel(playerLevelingSystem.currentLevel);
        playerLevelingSystem.experience = 0;
        playerLevelingSystem.experience = (oldEXP - newexp);
        currentLevel.text = playerLevelingSystem.currentLevel.ToString();
        setExpSliderMaxValue();
        Instantiate(onLevelUpEffect, transform.position, Quaternion.identity);
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

    private void setExpSliderMaxValue()
    {
        int nextLevelExpRange = playerLevelingSystem.GetXPforLevel(playerLevelingSystem.currentLevel + 1);
        slider.SetMaxExp(nextLevelExpRange);
    }

    public void updateExperience(int exp)
    {
        playerLevelingSystem.AddExp(exp);
        slider.SetExperience(playerLevelingSystem.experience);
        float levelPercentage = playerLevelingSystem.experience * 100.0f / playerLevelingSystem.GetXPforLevel(playerLevelingSystem.currentLevel + 1);
        currentLevelPercentage.text = String.Format("{0:0.00}", levelPercentage) + "%";
    }

    public void SavePlayer()
    {
        Statics.SavePlayerData(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = Statics.LoadPlayer();

        currentHealth = data.health;
        slider.SetHealth(data.health);
        playerLevelingSystem.experience = data.experience;
        slider.SetExperience(data.experience);
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        transform.position = position;
    }
}
