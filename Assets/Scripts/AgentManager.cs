using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    public GameObject agent;
    public AIControl agentScript;
    int layerMask;

    // Start is called before the first frame update
    void Start()
    {
        //select only the 'PathFinding' layer to get a room transform
        layerMask = 1 << 9;
        agent = GameObject.FindGameObjectWithTag("AI");
        agentScript = agent.GetComponent<AIControl>();
    }

    // Update is called once per frame
    void Update()
    {
        //on mouse click get the target position via RayCastHit
        if (Input.GetMouseButtonDown(0))
        {
            Ray goalTarget = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
                
                //send this hit location to the respective AI function
                //NavMesh function
                if (agentScript.NavMeshActive)
                {
                    if (Physics.Raycast(goalTarget, out hit, 10000))
                    {
                        agentScript.NavAgent.SetDestination(hit.point);
                    }
                }

                //Rule Based Function
                if (!agentScript.NavMeshActive)
                {
                    if (Physics.Raycast(goalTarget, out hit, 10000, layerMask))
                    {
                        agentScript.agentSearch(hit.transform);
                    }
                }
        }            
    }
}
