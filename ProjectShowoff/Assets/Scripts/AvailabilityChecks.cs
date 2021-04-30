using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailabilityChecks : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach(string device in Microphone.devices)
        {
            Debug.Log(device);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
