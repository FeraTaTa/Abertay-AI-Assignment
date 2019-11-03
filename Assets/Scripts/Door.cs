using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int doorMarked;
    public bool doorLocked;
    public List<Room> roomsAttached;
    public Material markedMaterial;
    public Material lockedMaterial;
    Renderer doorRenderer;

    // Start is called before the first frame update
    void Start()
    {
        doorMarked = 0;
        doorLocked = false;
        doorRenderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //on run-time create a list of rooms that are attached to each door
        if (other.tag == "Room")
        {
            roomsAttached.Add(other.gameObject.GetComponent<Room>());
        }
    }

    public void updateColour()
    {
        if (doorMarked == 1)
        {
            //set blue material
            doorRenderer.material = markedMaterial;
        }

        if (doorLocked == true)
        {
            //set red material
            doorRenderer.material = lockedMaterial;
        }
    }
}
