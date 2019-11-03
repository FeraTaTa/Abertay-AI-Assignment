using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControl : MonoBehaviour
{
    public bool NavMeshActive;
    public UnityEngine.AI.NavMeshAgent NavAgent;
    public List<Door> immediateDoors;
    public Transform previousRoomOnDoorEntry;
    public Transform nextRoomOnDoorExit;
    public Transform currentRoom;
    //    public Transform nextRoom;
    //    public Vector3 currentDoorPosition;
    //    Transform lastRoom;
    //    int roomNumTracker;

    // Start is called before the first frame update
    void Start()
    {
        NavMeshActive = false;
        NavAgent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        //roomNumTracker = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if NavMesh mode is not selected (RBS is active)
        if (!NavMeshActive)
        {
            //when the agent crosses a door
            if (other.tag == "Door")
            {
                Debug.Log("Hit door: " + other.name);
                previousRoomOnDoorEntry = currentRoom;
                //                //Get current door position - needed?
                //                currentDoorPosition = other.gameObject.transform.position;

                //get current door script
                Door currentDoor = other.GetComponent<Door>();

                //when the agent passes through a room increase the rooms marker count
                currentDoor.doorMarked++;
                //if the count is 2 that means the agent has passed through twice and this door is now locked (dead end)
                if (currentDoor.doorMarked >= 2)
                {
                    currentDoor.doorLocked = true;
                }
                currentDoor.updateColour();

                //                //TODO change this to choose doors that are marked 0 before doors marked 1

                //to find the next room iterate over the two rooms that are attached to the current door
                //since every door is only attached to 2 rooms do NOT go to the previous room
                for (int selector = 0; selector < currentDoor.roomsAttached.Count; selector++)
                {
                    //if the selected room is NOT the same as the last room then enter that room
                    if (currentDoor.roomsAttached[selector].transform != previousRoomOnDoorEntry)
                    {
                        //                        lastRoom = currentRoom;
                        nextRoomOnDoorExit = currentDoor.roomsAttached[selector].transform;
                        this.transform.position = nextRoomOnDoorExit.position;

                        //                        //calculateRoomNumber(currentRoom.GetComponent<Room>());
                        //                        Debug.Log("Go to room case 1: " + currentRoom);
                        //                        break;
                    }
                //                    else if (selector == currentDoor.roomsAttached.Count - 1)//???
                //                    {
                //                        lastRoom = currentRoom;
                //                        currentRoom = currentDoor.roomsAttached[selector].transform;

                //                        Debug.Log("Go to room case 2: " + currentRoom);
                //                        break;
                //                    }
                }
            }

            //when the agent enters a room (esspecially first room)
            if (other.tag == "Room")
            {
                Debug.Log("In room: " + other.name);
                //                lastRoom = Current`
                //once the agent enters a room get the transform of the room
                currentRoom = other.transform;
                //get list of doors attached to the current room
                immediateDoors = currentRoom.GetComponent<Room>().doorsAttached;
                //                //select and go to the next unlocked door
                //                //goToNextUnlockedDoor();
                //            }
            }
        }
    }
        //    void calculateRoomNumber(Room room)
        //    {
        //        //if the room number is 0 give it a new number
        //        if (room.distanceFromStart == 0)
        //        {
        //            room.distanceFromStart = roomNumTracker;
        //            roomNumTracker++;
        //    }

    public void agentSearch(Vector3 goal)
    {
        if (false) //TODO make it within some range of goal ((this.transform.position - goal).magnitude < 5)
        {
            //goal reached
        }
        else
        {
            //if not reached goal room select and go through the next unlocked door
            goToNextUnlockedDoor();
        }
    }

    private void goToNextUnlockedDoor()
    {
        bool allDoorsMarked = false;

        //iterate over the doors connected to the current room
        for (int selector = 0; selector < immediateDoors.Count; selector++)
        {
            //get the script attached to the currently selected door
            Door currentDoor = immediateDoors[selector].GetComponent<Door>();
            //if this door has not not been travelled through yet, go to it
            if(currentDoor.doorMarked == 0)
            {
                this.transform.position = (immediateDoors[selector].transform.position);
                break;
            }
            //if all doors have been travelled through set flag
            else if(selector == immediateDoors.Count - 1)
            {
                Debug.Log("All door marked");
                allDoorsMarked = true;
            }
        }
        if (allDoorsMarked)
        {
            //if all doors are marked then the agent has taken every available path, 
            //iterate over the doors again and take any available unlocked route
            for (int selector = 0; selector < immediateDoors.Count; selector++)
            {
                //get the script attached to the currently selected door
                Door currentDoor = immediateDoors[selector].GetComponent<Door>();
                //if the selected door is not locked travel to it
                if (!currentDoor.doorLocked)
                {
                    this.transform.position = (immediateDoors[selector].transform.position);
                    break;
                }
                //if selector makes it to the end of the door list without finding 
                //an unlocked door that means the agent is stuck
                else if (selector == immediateDoors.Count - 1)
                {
                    Debug.Log("Hit dead end!!!!!!");
                    break;
                }
            }
        }
    }
}


