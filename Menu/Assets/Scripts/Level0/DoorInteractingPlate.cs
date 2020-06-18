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
        if (type.name == "checkRed")
        {
            triggerScript.checkA = true;
            triggerScript.checkB = true;

        }

        if (type.name == "checkBlue")
        {

            triggerScript.checkC = true;
            triggerScript.checkD = true;
        }

    }


}
