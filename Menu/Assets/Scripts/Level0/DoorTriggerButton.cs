using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerButton : MonoBehaviour
{

    [SerializeField] private GameObject door;
    [SerializeField] private GameObject doorClosed;

    private IDoor doorA;
    private IDoor doorB;


    private void Awake()
    {
        doorA = door.GetComponent<IDoor>();
        doorB = doorClosed.GetComponent<IDoor>();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            doorA.openDoor();
            doorB.closeDoor();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            doorA.closeDoor();
            doorB.openDoor();
        }
    }
}
