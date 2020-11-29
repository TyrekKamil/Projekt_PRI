using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BigBulletController : MonoBehaviour
{
    public float speed = 15f;
    public Rigidbody2D rigidbody;

    public Animator transition;
    void Start()
    {
        rigidbody.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            StartCoroutine(LoadLevel());
        }
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Level1-Area");
    }

}
