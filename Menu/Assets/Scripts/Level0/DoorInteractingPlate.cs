using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractingPlate : MonoBehaviour
{
    public GameObject door;
    public GameObject type;
    DoorTriggerButton triggerScript;

    void Start()
    {
        triggerScript = door.GetComponent<DoorTriggerButton>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (type.name == "blue")
        {
            triggerScript.checkA = true;
            Debug.Log("checkA");
        }
        if (type.name == "redMid")
        {
            triggerScript.checkB = true;
            Debug.Log("checkB");

        }
        if (type.name == "GreenMid")
        {
            triggerScript.checkC = true;
            Debug.Log("checkC");

        }

        if (type.name == "YellowEnd")
        {
            triggerScript.checkD = true;
            Debug.Log("checkD");

        }

    }


}
