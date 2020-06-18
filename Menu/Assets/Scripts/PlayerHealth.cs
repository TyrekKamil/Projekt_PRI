using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int health = 3;

    public void ChangeHealth(int hit)
    {
        health -= hit;
    }

    public int DisplayHealth()
    {
        return health;
    }
}
