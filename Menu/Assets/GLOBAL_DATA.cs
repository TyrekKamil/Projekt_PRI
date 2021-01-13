﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLOBAL_DATA : MonoBehaviour
{
    public static GLOBAL_DATA Instance;

    public int HP;
    public int Level;
    public int SkillPoints;
    public int XP;
    public int areaBestScore = 0;
    public int expAfterPuzzle = 0;
    public bool winPuzzle;
    public bool[] chestOpened = new bool[3];
    public int actualChestIndex;

    void Awake()
    {
        if (Instance == null)
        {
            Debug.Log("if");
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
                        Debug.Log("elif");

            Destroy(gameObject);
        }
    }
}

