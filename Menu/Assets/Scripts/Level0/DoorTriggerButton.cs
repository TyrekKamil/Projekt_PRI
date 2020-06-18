using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerButton : MonoBehaviour
{

    [SerializeField] private GameObject door;
    [SerializeField] private GameObject doorClosed;

    private IDoor doorA;
    private IDoor doorB;

    public bool checkA = false;
    public bool checkB = false;
    public bool checkC = false;
    public bool checkD = false;


    private void Awake()
    {
        doorA = door.GetComponent<IDoor>();
        doorB = doorClosed.GetComponent<IDoor>();

    }
    private void Update()
    {
        if (checkA == true && checkB == true && checkC == true && checkD == true)
        {
            // doorA.openDoor();
            // doorB.closeDoor();
            Debug.Log("aaa");
        }

    }
}
