using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjAfterDel : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            StartCoroutine("WaitForSec");
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
