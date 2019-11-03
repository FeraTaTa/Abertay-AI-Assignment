using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int doorMarked;
    public bool doorLocked;
    public List<Room> roomsAttached;

    // Start is called before the first frame update
    void Start()
    {
        doorMarked = 0;
        doorLocked = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //on run-time create a list of rooms that are attached to each door
        if (other.tag == "Room")
        {
            roomsAttached.Add(other.gameObject.GetComponent<Room>());
        }
    }
}
