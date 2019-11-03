using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int distanceFromStart;
    public bool hasBeenVisited;
    public List<Door> doorsAttached;

    // Start is called before the first frame update
    void Start()
    {
        distanceFromStart = 0;
        hasBeenVisited = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //on run-time create a list of doors that are attached to each room
        if (other.tag == "Door")
        {
            doorsAttached.Add(other.gameObject.GetComponent<Door>());
        }
    }
}
