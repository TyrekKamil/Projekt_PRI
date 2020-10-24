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
    public GameObject onLvlUpPrefabEffect;

    private GameObject onLvlUpEffect;



    void Start()
    {
        GameEvents.SaveInitiated += SavePlayerData;
        GameEvents.LoadInitiated += LoadPlayerData;

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

        if (onLvlUpEffect)
        {
            onLevelUpEffect.transform.position = transform.position;
        }
    }


    public void OnLevelUp()
    {
        print("New level! Now you are level: " + playerLevelingSystem.currentLevel);
        int oldEXP = playerLevelingSystem.experience;
        int newexp = playerLevelingSystem.GetXPforLevel(playerLevelingSystem.currentLevel);
        playerLevelingSystem.experience = 0;
        playerLevelingSystem.experience = (oldEXP - newexp);
        setExpSliderMaxValue();
        onLvlUpEffect = Instantiate(onLvlUpPrefabEffect);
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
    private void SavePlayerData()
    {
        SerializablePlayer serializedPlayer = new SerializablePlayer()
        {
            health = this.currentHealth,
            experience = playerLevelingSystem.experience,
            level = playerLevelingSystem.currentLevel,
            positionX = transform.position.x,
            positionY = transform.position.y,
            positionZ = transform.position.z,

        };

        SaveLoad.Save<SerializablePlayer>(serializedPlayer, "PlayerStats");
    }

    private void LoadPlayerData()
    {
        if (SaveLoad.SaveExists("PlayerStats"))
        {
            SerializablePlayer playerData = SaveLoad.Load<SerializablePlayer>("PlayerStats");

            currentHealth = playerData.health;
            playerLevelingSystem.experience = playerData.experience;
            playerLevelingSystem.currentLevel = playerData.level;

            Vector3 position;
            position.x = playerData.positionX;
            position.y = playerData.positionY;
            position.z = playerData.positionZ;

            transform.position = position;
        }

      
    }
}

[System.Serializable]
public class SerializablePlayer
{
    public int health { get; set; }
    public int experience { get; set; }
    public int level { get; set; }
    public float positionX { get; set; }
    public float positionY { get; set; }
    public float positionZ { get; set; }
}
