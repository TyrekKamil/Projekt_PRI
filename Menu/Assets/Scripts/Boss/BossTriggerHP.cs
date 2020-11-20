using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTriggerHP : MonoBehaviour
{
    public GameObject bossHP;
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            this.bossHP.SetActive(true);
            Destroy(this);
        }
    }


}
