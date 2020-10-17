using UnityEngine;

public class PlayerUIUpdates : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

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
        setExpSliderMaxValue();
    }

   /* private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            playerLevelingSystem.AddExp(30);
            slider.SetExperience(playerLevelingSystem.experience);
        }
    }
   */
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
    }
}
