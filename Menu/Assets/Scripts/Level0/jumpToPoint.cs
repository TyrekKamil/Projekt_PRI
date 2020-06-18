using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpToPoint : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            player.transform.position = respawnPoint.transform.position;
        }

    }
}
