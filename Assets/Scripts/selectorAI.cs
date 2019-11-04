using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class selectorAI : MonoBehaviour
{
    AIControl agent;
    UnityEngine.AI.NavMeshAgent agentNavComponent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GameObject.FindGameObjectWithTag("AI").GetComponent<AIControl>();
        agentNavComponent = GameObject.FindGameObjectWithTag("AI").GetComponent<UnityEngine.AI.NavMeshAgent>();
        updateText();
    }

    public void toggleActiveAI()
    {
        agent.NavMeshActive = !agent.NavMeshActive;
        updateText();
    }

    void updateText()
    {
        if (agent.NavMeshActive)
        {
            this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "NavMesh Selected";
            agentNavComponent.enabled = true;
        }
        else
        {
            this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Rule-Based Selected";
            agentNavComponent.enabled = false;
        }
    }
}
