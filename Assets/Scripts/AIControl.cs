using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControl : MonoBehaviour
{
    public bool NavMeshActive;
    public UnityEngine.AI.NavMeshAgent NavAgent;

    List<Door> immediateDoors;
    Transform previousRoomOnDoorEntry;
    Transform currentRoom;
    Room lastBranch;
    bool setOnce;
    bool goalHitRBS;
    Transform targetRooom;
    int goalAccuracy;
    Tween currentMovement;

    //int roomNumTracker;

    // Start is called before the first frame update
    void Start()
    {
        goalAccuracy = 5;
        goalHitRBS = false;
        setOnce = true;
        NavMeshActive = false;
        NavAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //roomNumTracker = 0;
    }

    private IEnumerator searchForRoom()
    {
        while (!goalHitRBS)
        {
            //goal reached if the agents position is within some accuracy to the target position
            if ((targetRooom.position - transform.position).magnitude < goalAccuracy)
            {
                Debug.Log("Goal Reached");
                goalHitRBS = true;
            }
            //if goal not reached and RBS is active search for goal
            else if (!goalHitRBS)
            {
                        Debug.Log("Search: " + (currentMovement==null));
                if (currentMovement == null)
                {
                    goToNextUnlockedDoor();

                }
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if NavMesh mode is not selected (RBS is active)
        if (!NavMeshActive)
        {
            //when the agent crosses a door
            if (other.tag == "Door")
            {
                //Debug.Log("Hit door: " + other.name);
                previousRoomOnDoorEntry = currentRoom;

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

                //to find the next room iterate over the two rooms that are attached to the current door
                //since every door is only attached to 2 rooms do NOT go to the previous room
                for (int selector = 0; selector < currentDoor.roomsAttached.Count; selector++)
                {
                    //if the selected room is NOT the same as the last room then enter that room
                    if (currentDoor.roomsAttached[selector].transform != previousRoomOnDoorEntry)
                    {
                        currentRoom = currentDoor.roomsAttached[selector].transform;
                        onTransformMove(currentRoom.position);
                        immediateDoors = currentRoom.GetComponent<Room>().doorsAttached;

                        // Get all open doors in the new current room
                        List<Door> openDoorList = new List<Door>();
                        foreach (Door door in immediateDoors)
                        {
                            if (door.doorLocked != true)
                            {
                                // Add Open doors
                                openDoorList.Add(door);
                            }
                        }

                        if (openDoorList.Count > 2)
                        {
                            // StoreLastBranch
                            lastBranch = currentRoom.GetComponent<Room>();
                            //Debug.Log("Last branch set to " + lastBranch.name);
                        } else if (openDoorList.Count == 0)
                        {
                            // Is at a dead end
                            Debug.Log("On Dead end " + currentRoom.name);
                            onTransformMove(lastBranch.transform.position);
                            currentRoom = lastBranch.transform;
                            immediateDoors = currentRoom.GetComponent<Room>().doorsAttached;
                        }
                        break;
                    }
                }
            }

            //when the agent enters a room (esspecially first room)
            if (other.tag == "Room")
            {
                //Debug.Log("In room: " + other.name);
                if (setOnce)
                {
                    currentRoom = other.transform;
                    immediateDoors = currentRoom.GetComponent<Room>().doorsAttached;
                    setOnce = false;
                }
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

    public void agentSearch(Transform goal)
    {
        Debug.Log("RBS Target is: " + goal.name);
        goalHitRBS = false;
        targetRooom = goal;
        //start searching for the target room
        StartCoroutine(searchForRoom());
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
                onTransformMove(immediateDoors[selector].transform.position);
                break;
            }
            //if all immediate doors have been travelled through set flag
            else if(selector == immediateDoors.Count - 1)
            {
                Debug.Log("All door marked in: "+currentRoom.name);
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
                    onTransformMove(immediateDoors[selector].transform.position);
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

    void onTransformMove(Vector3 positionTarget)
    {
        Debug.Log("onTransformMove");
        this.GetComponent<Collider>().enabled = false;

        currentMovement = transform.DOMove(positionTarget, 0.65f).SetEase(Ease.InOutQuad).OnComplete(() => {
            Debug.Log("complete tween");
            this.GetComponent<Collider>().enabled = true;
            currentMovement = null;
        });

    }



    //int chooseRandomDoor(Room room)
    //{
    //    List<Door> doorList = room.doorsAttached;
    //    int doorIndex;
    //    bool doorChosen = false;
    //    do
    //    {
    //        doorIndex = Random.Range(0, doorList.Count);
    //        if (doorList[doorIndex].doorLocked == true)
    //        {
    //            doorList.RemoveAt(doorIndex);
    //        }
    //        else
    //        {
    //            doorChosen = true;
    //            return doorIndex;
    //        }
    //    } while (!doorChosen);
    //}
}


