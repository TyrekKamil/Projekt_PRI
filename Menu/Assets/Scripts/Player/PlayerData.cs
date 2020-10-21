using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int health;
    public int experience;
    public int level;
    public float[] position;

    public PlayerData(PlayerUIUpdates player)
    {
        health = player.currentHealth;
        experience = player.playerLevelingSystem.experience;
        level = player.playerLevelingSystem.GetLevelForXP(experience);
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
