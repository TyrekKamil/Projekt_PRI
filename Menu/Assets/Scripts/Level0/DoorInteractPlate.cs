using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractPlate : MonoBehaviour
{

    [SerializeField] private DoorSetActive door;
    [SerializeField] private DoorSetActive doorClosed;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            door.openDoor();
            doorClosed.closeDoor();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            door.closeDoor();
            doorClosed.openDoor();
        }
    }
}
