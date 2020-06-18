using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSetActive : MonoBehaviour, IDoor
{

    private bool isOpen = false;
    public void openDoor()
    {
        gameObject.SetActive(false);
    }

    public void closeDoor()
    {
        gameObject.SetActive(true);
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            openDoor();
        }
        else
        {
            closeDoor();
        }
    }


}
