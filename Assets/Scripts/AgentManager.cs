using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    public GameObject agent;
    public AIControl agentScript;
    
    private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        agent = GameObject.FindGameObjectWithTag("AI");
        agentScript = agent.GetComponent<AIControl>();
        moveSpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        //on mouse click get the target position via RayCastHit
        if (Input.GetMouseButtonDown(0))
        {
            Ray goalTarget = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
                
            if (Physics.Raycast(goalTarget, out hit, 10000))
            {
                //send this hit location to the respective AI function

                //NavMesh function
                if (agentScript.NavMeshActive)
                {
                    agentScript.NavAgent.SetDestination(hit.point);
                }

                //Rule Based Function
                if (!agentScript.NavMeshActive)
                {
                    agentScript.agentSearch(hit.point);
                }
            }
        }            
    }

}
