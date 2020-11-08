using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLOBAL_DATA : MonoBehaviour
{
    public static GLOBAL_DATA Instance;

    public int HP;
    public int Level;
    public int XP;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
