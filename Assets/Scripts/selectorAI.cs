using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class selectorAI : MonoBehaviour
{
    AIControl agent;
    bool isNavMeshActive;
    UnityEngine.AI.NavMeshAgent agentNavComponent;
    TextMeshProUGUI sceneText;
    // Start is called before the first frame update
    void Start()
    {
        updateScene();
    }

    public void loadScene(string sceneName)
    {
        isNavMeshActive = agent.NavMeshActive;
        SceneManager.LoadScene(sceneName);
        SceneManager.sceneLoaded += onSceneLoaded;
        
    }

    void onSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= onSceneLoaded;
        updateScene();
    }

    void updateScene()
    {
        agent = GameObject.FindGameObjectWithTag("AI").GetComponent<AIControl>();
        Debug.Log("Current Scene is: " + SceneManager.GetActiveScene().name);
        sceneText = GameObject.FindGameObjectWithTag("sceneText").GetComponent<TextMeshProUGUI>();
        Debug.Log(sceneText);
        if (SceneManager.GetActiveScene().name == "NavScene")
        {
            agent.NavMeshActive = true;
            sceneText.text = "Nav Scene";
        }
        else if(SceneManager.GetActiveScene().name == "RBSScene")
        {
            agent.NavMeshActive = false;
            sceneText.text = "RBS Scene";

        }
    }
}
