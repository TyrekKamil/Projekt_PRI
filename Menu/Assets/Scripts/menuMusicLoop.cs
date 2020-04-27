using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuMusicLoop : MonoBehaviour
{
    void Awake()
    {
      DontDestroyOnLoad(transform.gameObject);
    }
}
