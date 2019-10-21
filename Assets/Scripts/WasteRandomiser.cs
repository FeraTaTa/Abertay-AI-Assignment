using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasteRandomiser : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        var rnd = new System.Random();
        int presence = rnd.Next(1,10);
        
        Debug.Log(presence);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
