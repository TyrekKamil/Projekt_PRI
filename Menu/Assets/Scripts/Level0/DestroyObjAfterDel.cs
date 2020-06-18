using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjAfterDel : MonoBehaviour
{
    public GameObject light;
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
        if (gameObject.name == "falliingParts9")
        {
            Destroy(gameObject);
            Destroy(light);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
