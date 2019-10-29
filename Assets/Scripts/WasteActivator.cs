using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasteActivator : MonoBehaviour
{
    WasteSelector[] nuclearWasteList;

    [Range(1, 10)]
    public int ChanceOfActive = 5;

    // Start is called before the first frame update
    void Start()
    {
        nuclearWasteList = GetComponentsInChildren<WasteSelector>(true);
        Debug.Log("hello");
        foreach (WasteSelector waste in nuclearWasteList) {
            
            
            float presence = Random.Range(0f, 10f);
            Debug.Log(presence);
            if (presence > ChanceOfActive)
            {
                waste.transform.gameObject.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
