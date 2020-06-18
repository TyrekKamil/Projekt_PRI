using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSetActive : MonoBehaviour
{

    public void openDoor()
    {
        gameObject.SetActive(false);
    }

    public void closeDoor()
    {
        gameObject.SetActive(true);
    }


}
