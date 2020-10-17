using UnityEngine;

public class PlayerUIUpdates : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public PlayerHpExpUI slider;
    public PlayerLevelingSystem playerLevelingSystem;

    void Start()
    {
        playerLevelingSystem = new PlayerLevelingSystem(4, OnLevelUp);
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
