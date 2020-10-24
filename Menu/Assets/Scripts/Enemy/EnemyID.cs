using UnityEngine;

public class EnemyID : MonoBehaviour
{
    public string ID { get; private set; }

    private void Awake()
    {
        ID = transform.position.sqrMagnitude + "-" + name + "-" + transform.GetSiblingIndex();
    }
}
