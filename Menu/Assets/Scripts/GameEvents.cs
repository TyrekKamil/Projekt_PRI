using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static Action SaveInitiated;
    public static Action LoadInitiated;
    public static Action SaveSceneInitiated;
    public static Action LoadSceneInitiated;

    public static void OnSaveInitiated()
    {
        SaveInitiated?.Invoke(); 
    }

    public static void OnLoadInitiated()
    {
        LoadInitiated?.Invoke();
    }

    public static void OnSaveSceneInitiated()
    {
        SaveSceneInitiated?.Invoke();
    }

    public static void OnLoadSceneInitiated()
    {
        LoadSceneInitiated?.Invoke();
    }
}
