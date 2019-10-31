using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WasteActivator : MonoBehaviour
{
    WasteSelector[] nuclearWasteList;
    public NavMeshSurface surface;

    [Range(1, 10)]
    public int ChanceOfActive = 5;

    // Start is called before the first frame update
    void Start()
    {
        
        nuclearWasteList = GetComponentsInChildren<WasteSelector>(true);
        
        foreach (WasteSelector waste in nuclearWasteList) {
            
            
            float presence = Random.Range(0f, 10f);

            if (presence > ChanceOfActive)
            {
                waste.transform.gameObject.SetActive(true);
            }
        }

        //update navmesh
        surface.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
