using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static Action SaveInitiated;
    public static Action LoadInitiated;

    public static void OnSaveInitiated()
    {
        SaveInitiated?.Invoke(); 
    }

    public static void OnLoadInitiated()
    {
        LoadInitiated?.Invoke();
    }
}
