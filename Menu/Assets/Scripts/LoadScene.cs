using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
 public int index;
    public void SceneLoader() {
        SceneManager.LoadScene(index);
    }
}
