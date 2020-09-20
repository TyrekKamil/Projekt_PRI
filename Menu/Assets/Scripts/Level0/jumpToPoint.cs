using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class jumpToPoint : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;
    public GameObject uiObject;

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            player.transform.position = respawnPoint.transform.position;
            uiObject.SetActive(true);
            StartCoroutine("WaitForSec");
        }

    }

     IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(2);
        uiObject.SetActive(false);
    }
}
