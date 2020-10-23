using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    private List<GameObject> enemies = new List<GameObject>();

    private void Start()
    {
        
    }

    private void Update()
    {
        Debug.Log(enemies.Capacity);
    }
}
