using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BigBulletController : MonoBehaviour
{
    public float speed = 1f;
    public Rigidbody2D rigidbody;
    public Animator transition;
    private float scaleSpeed = 1.5f;
    void Update()
    {
        if (gameObject.active)
        {
            transform.localScale = transform.localScale + new Vector3(scaleSpeed, scaleSpeed, scaleSpeed);
        }
        if (transform.localScale.x > 14)
        {
            StartCoroutine(LoadLevel());
        }
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(3);
        SceneManager.LoadSceneAsync("Level1-Area");
    }

}
