using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

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

    private void Update()
    {
        currentLevel.text = playerLevelingSystem.currentLevel.ToString();

        float levelPercentage = playerLevelingSystem.experience * 100.0f / playerLevelingSystem.GetXPforLevel(playerLevelingSystem.currentLevel + 1);
        currentLevelPercentage.text = String.Format("{0:0.00}", levelPercentage) + "%";

        slider.SetHealth(currentHealth);
        slider.SetExperience(playerLevelingSystem.experience);
    }


    public void OnLevelUp()
    {
        print("New level! Now you are level: " + playerLevelingSystem.currentLevel);
        int oldEXP = playerLevelingSystem.experience;
        int newexp = playerLevelingSystem.GetXPforLevel(playerLevelingSystem.currentLevel);
        playerLevelingSystem.experience = 0;
        playerLevelingSystem.experience = (oldEXP - newexp);
        setExpSliderMaxValue();
        Instantiate(onLevelUpEffect, transform.position, Quaternion.identity);
    }
   
    public void ChangeHealth(int hit)
    {
        currentHealth -= hit;
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
        currentHealth = maxHealth;
    }

    private void setExpSliderMaxValue()
    {
        int nextLevelExpRange = playerLevelingSystem.GetXPforLevel(playerLevelingSystem.currentLevel + 1);
        slider.SetMaxExp(nextLevelExpRange);
    }

    //Updates experience, experience slider and text in %
    public void updateExperience(int exp)
    {
        playerLevelingSystem.AddExp(exp);
       
    }

    public void SavePlayer()
    {
        SaveData.current.playerData.experience = playerLevelingSystem.experience;
        SaveData.current.playerData.level = playerLevelingSystem.currentLevel;
        SaveData.current.playerData.health = currentHealth;
        SaveData.current.playerData.positionX = transform.position.x;
        SaveData.current.playerData.positionY = transform.position.y;
        SaveData.current.playerData.positionZ = transform.position.z;
        SerializationManager.Save("Player", SaveData.current);
    }

    public void LoadPlayer()
    {
        SaveData.current = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/saves/Player.save");
        currentHealth = SaveData.current.playerData.health;
        playerLevelingSystem.experience = SaveData.current.playerData.experience;

        Vector3 position;
        position.x = SaveData.current.playerData.positionX;
        position.y = SaveData.current.playerData.positionY;
        position.z = SaveData.current.playerData.positionZ;

        transform.position = position;
    }
}
