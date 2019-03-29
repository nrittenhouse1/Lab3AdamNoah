using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResetScript : MonoBehaviour
{
    GameTracker gcInstance;

    void Start()
    {
        gcInstance = GameTracker.gcInstance;
        if(gcInstance.numPlayers <= 1)
        {
            this.GetComponent<Camera>().rect = new Rect(0, 0, 1, 1);
        }
    }


    void Update()
    {
        
    }
}
