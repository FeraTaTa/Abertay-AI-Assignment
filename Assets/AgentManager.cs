using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    //GameObject[] agents;
    public GameObject agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GameObject.FindGameObjectWithTag("AI");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Debug.Log(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 10000));
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 10000)) 
            {
                Debug.Log("mouse pos: " + Input.mousePosition);
                Debug.Log("the hit point is :" + hit.point);
                //foreach (GameObject a in agent)
                //{
                    agent.GetComponent<AIControl>().agent.SetDestination(hit.point);
                //}
            }
        }
    }
}
