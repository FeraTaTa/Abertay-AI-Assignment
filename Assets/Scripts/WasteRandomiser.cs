using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script will disable a predefined "nuclear waste" asset 
 * which acts as a random obstacle in the path of the agent. 
 * Whether the object is active or not is based on a comparison 
 * between a developer defined 'ChanceOfActive' and a randomly generated 'presence'
 */
public class WasteRandomiser : MonoBehaviour
{
    [Range(1, 10)]
    public int ChanceOfActive = 5;

    // Start is called before the first frame update
    void Start()
    {
        var rnd = new System.Random();
        int presence = rnd.Next(1,10);
        
        if (presence > ChanceOfActive)
        {
            this.transform.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
