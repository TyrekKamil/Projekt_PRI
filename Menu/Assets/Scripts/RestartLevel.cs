using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour
{
    public GameObject Player;

    void Start()
    {
        Player = (GameObject)Resources.Load(Application.dataPath + "/Resources/Player");
        Debug.Log(Application.dataPath + "/Resources/Player");
    }

    IEnumerator OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            Destroy(collision.gameObject);

            yield return new WaitForSeconds(2);

            Instantiate(Resources.Load("Player"));
        }
    }
}

