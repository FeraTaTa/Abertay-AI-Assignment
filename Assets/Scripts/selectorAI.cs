using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class selectorAI : MonoBehaviour
{
    AIControl agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GameObject.FindGameObjectWithTag("AI").GetComponent<AIControl>();
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
        }
        else
        {
            this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Rule-Based Selected";
        }
    }
}
